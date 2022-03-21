[![.NET](https://github.com/Noticia-Systems/Noticia.AspNetCore.AuthorizationEvaluationMiddleware/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Noticia-Systems/Noticia.AspNetCore.AuthorizationEvaluationMiddleware/actions/workflows/dotnet.yml)

# Noticia.AspNetCore.AuthorizationEvaluationMiddleware

This middleware provides the option to not execute requests whenever the `Authorization-Evaluation` header of a HTTP request is set to a truthy value.

Providing this header allows for a policy agent (like OpenPolicyAgent) to evaluate the request and when granted, preventing the HTTP request's further execution.

This workflow is useful when using client apps (i.e. SPAs like Angular) and you need to check whether the user has certain permissions to access resources within the client app, but without having to allow public access to the policy agent.

## Usage

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  ...
  
  app.UseAuthorizationEvaluation();
  
  ...
}
```