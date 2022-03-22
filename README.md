[![Nuget](https://img.shields.io/nuget/v/Noticia.AspNetCore.AuthorizationEvaluationMiddleware)](https://www.nuget.org/packages/Noticia.AspNetCore.AuthorizationEvaluationMiddleware/) [![.NET](https://github.com/Noticia-Systems/Noticia.AspNetCore.AuthorizationEvaluationMiddleware/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Noticia-Systems/Noticia.AspNetCore.AuthorizationEvaluationMiddleware/actions/workflows/dotnet.yml) [![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT) [![CodeQL](https://github.com/Noticia-Systems/Noticia.AspNetCore.AuthorizationEvaluationMiddleware/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/Noticia-Systems/Noticia.AspNetCore.AuthorizationEvaluationMiddleware/actions/workflows/codeql-analysis.yml)

This middleware provides the option to not execute requests whenever the `Authorization-Evaluation` header of a HTTP request is set to a truthy value.

Providing this header allows for a policy agent (like OpenPolicyAgent) to evaluate the request and when granted, preventing the HTTP request's further execution.

This workflow is useful when using client apps (i.e. SPAs like Angular) and you need to check whether the user has certain permissions to access resources within the client app, but without having to allow public access to the policy agent.

NOTE: This package currently **requires** the usage of a policy middleware. This library only prevents requests from being executed whenever the policy server grants the request. Whenever the request is granted and this middleware prevents the execution, the request will receive a `204 (No Content)` response.

## Installation

```
dotnet add package Noticia.AspNetCore.AuthorizationEvaluationMiddleware
```

## Usage

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  ...
  
  app.UseAuthorizationEvaluation();
  
  ...
}
```
