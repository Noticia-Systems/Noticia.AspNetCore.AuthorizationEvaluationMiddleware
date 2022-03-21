using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Noticia.AspNetCore.AuthorizationEvaluationMiddleware;

/// <summary>
/// Middleware allowing authorization evaluation by catching an evaluation request before executing, but not preventing the policy agent from granting or denying the request.
/// </summary>
public class AuthorizationEvaluationMiddleware
{
    #region Fields

    /// <summary>
    /// Function for further processing the HTTP request.
    /// </summary>
    private readonly RequestDelegate next;

    #endregion
   
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationEvaluationMiddleware"/> class.
    /// </summary>
    /// <param name="next">Function for further processing the HTTP request.</param>
    public AuthorizationEvaluationMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    
    #endregion

    #region Methods

    /// <summary>
    /// Checks whether the Authorization-Evaluation header is present and if its value is truthy and preventing further execution of this request if so.
    /// </summary>
    /// <param name="context"><see cref="HttpContext"/> for this request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization-Evaluation"))
        {
            if (bool.TryParse(context.Request.Headers["Authorization-Evaluation"], out bool isAuthorizationEvaluationRequest) && isAuthorizationEvaluationRequest || context.Request.Headers["Authorization-Evaluation"] == "1")
            {
                // OPA package would 403 if not granted, we just prevent the request from executing.
                context.Response.StatusCode = StatusCodes.Status204NoContent;

                return;
            }
        }
        
        await this.next(context);
    }

    #endregion
    
}