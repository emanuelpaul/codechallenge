using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeChallenge.API.Infrastructure
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            object[] methodCustomAttributes = context.MethodInfo.GetCustomAttributes(false);
            List<object> customAttributes = context.MethodInfo
                                                   .DeclaringType
                                                   .GetCustomAttributes(false)
                                                   .ToList();
            customAttributes.AddRange(methodCustomAttributes);

            bool allowAnonymous = customAttributes.OfType<AllowAnonymousAttribute>().Any();

            if (!allowAnonymous)
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>> {
                    new Dictionary<string, IEnumerable<string>> {{ "Bearer", new[] { "apiKey" } }}
                };
            }
        }
    }
}
