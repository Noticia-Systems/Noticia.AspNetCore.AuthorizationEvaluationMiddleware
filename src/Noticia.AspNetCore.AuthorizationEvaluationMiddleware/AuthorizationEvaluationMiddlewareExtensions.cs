using Microsoft.AspNetCore.Builder;

namespace Noticia.AspNetCore.AuthorizationEvaluationMiddleware;

/// <summary>
/// Extension methods for enabling the middleware.
/// </summary>
public static class AuthorizationEvaluationMiddlewareExtensions
{
    #region Static Methods

    /// <summary>
    /// Applies the authorization evaluation middleware during configuration.
    /// </summary>
    /// <param name="builder"><see cref="IApplicationBuilder"/> used in Configure().</param>
    /// <returns><see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseAuthorizationEvaluation(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationEvaluationMiddleware>();
    }

    #endregion
}