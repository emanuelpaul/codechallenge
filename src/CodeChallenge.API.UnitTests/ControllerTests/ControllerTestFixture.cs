using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CodeChallenge.API.Automapper;

namespace CodeChallenge.API.UnitTests.ControllerTests
{
    class ControllerTestFixture : IDisposable
    {
        public ControllerTestFixture()
        {
            Mapper.Initialize(config => config.AddProfile<CompanyProfile>());
        }

        public void Dispose()
        {
            
        }
    }
}
