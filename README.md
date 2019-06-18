## Description
* Solution has 4 projects
* API - used for companies CRUD, writes to a sql database, secured using JWT
  * For simplicity users are now hardcoded but asp.net net core identity can be added for example
    * Users: Username = "heisenberg", Password = "IAmTheOneWhoKnocks" and Username = "tyrion", Password="IDrinkAndIKnowThings"
  * Swagger and Swagger UI are used in API
* Angular App - UI used for companies CRUD, it uses the API. User neeeds to be logged in to be able to access companies.
* Database project - used to retain information about database structure and to seed data
* Unit tests - has some unit tests, more are needed for better coverage

## Requirements
* [Node v10.16.0 LTS or higher](https://nodejs.org/en/)
* [Sql server 2014 or higher](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [.NET CORE SDK 2.2](https://dot.net)
* [Visual Studio 2017 or higher with .NET Core and ASP.NET workloads AND (optional)SSDT](https://visualstudio.com)

## Setup
* Create a new database
* Publish the database project(if SSDT installed) or use the script from root "sqlscripts.sql"
* Update the connection string from src/CodeChallenge.API/appsettings.json in section ConnectionStrings key CodeChallengeDb with your connection string
* Start both projects in visual studio(Multiple startup projects) CodeChallenge.API and CodeChallenge.Angular 

## Optional - run applications from command line
* Go in src\CodeChallenge.API and run command: "dotnet run"
* Go in src\CodeChallenge.Angular and run command "dotnet run"
* Open in a browser: [https://localhost:5001](https://localhost:5001)
* Run tests from command line: go to src\CodeChallenge.API.UnitTests and run command "dotnet test"

## SSL potential issues
* Both projects are configured to work on https
* In case you have problems running on https please check [this](https://www.hanselman.com/blog/DevelopingLocallyWithASPNETCoreUnderHTTPSSSLAndSelfSignedCerts.aspx)
### How to run applications on http(if SSL issues cannot be fixed)
* Both projects projects work also on http. API on [http://localhost:58849](http://localhost:58849/swagger/index.html) and angular app on [http://localhost:49323](http://localhost:49323)
* Go to src\CodeChallenge.Angular\ClientApp\src\main.ts and replace 'https://localhost:44302' with 'http://localhost:58849'
* Build
* Open in a browser angular client app: [http://localhost:49323](http://localhost:49323)