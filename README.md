# ASP.NET Core Identity with MongoDB

A C# learning API that uses MongoDB for ASP.NET Core Identity and provides student and course endpoints.

## Code guide

`Controllers/UserController.cs` handles registration and token creation. `Features/Users/` contains identity models and repository code. `Features/Students/` and `Features/Courses/` contain the domain exercises. `Program.cs` configures dependency injection, JWT validation and MongoDB identity storage.

## Local setup

The project targets the .NET version recorded in `MongoDbExample.csproj`. Install a matching development SDK, start a local MongoDB instance, and configure `SchoolDatabaseSettings__ConnectionString` and `SchoolDatabaseSettings__DatabaseName` through environment variables.

Set a new random `Jwt__SigningKey` in the environment before starting. The same value signs and validates JWTs. `.env.example` lists the variable; ASP.NET Core does not automatically load that file.

```bash
dotnet restore
dotnet build
dotnet run
```

Use the URL printed by the development server; Swagger is enabled in Development.

## Status

Historical learning project. Token issuer/audience validation and production security policies are not complete. No automated authentication integration coverage or current dependency audit is claimed. Any signing key previously copied from the source must be replaced; removing a literal does not revoke existing credentials or tokens.
