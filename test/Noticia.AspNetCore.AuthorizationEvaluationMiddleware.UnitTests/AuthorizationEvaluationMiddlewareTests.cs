using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Noticia.AspNetCore.AuthorizationEvaluationMiddleware.UnitTests;

public class AuthorizationEvaluationMiddlewareTests
{
    #region Fields

    private readonly AuthorizationEvaluationMiddleware authorizationEvaluationMiddleware;

    private readonly Mock<RequestDelegate> requestDelegateMock;

    #endregion

    #region Constructors

    public AuthorizationEvaluationMiddlewareTests()
    {
        this.requestDelegateMock = new Mock<RequestDelegate>();
        this.requestDelegateMock.Setup(_ => _(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

        this.authorizationEvaluationMiddleware = new AuthorizationEvaluationMiddleware(requestDelegateMock.Object);
    }

    #endregion

    #region Methods

    [Fact]
    public void Should_ContinueRequest_When_AuthorizationEvaluationHeaderMissing()
    {
        var httpContext = new DefaultHttpContext();
        
        this.authorizationEvaluationMiddleware.InvokeAsync(httpContext);

        this.requestDelegateMock.Verify(_ => _(httpContext), Times.Once);
    }
    
    [Theory]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("2")]
    [InlineData("")]
    public void Should_ContinueRequest_When_AuthorizationEvaluationHeaderNotTruthy(string headerValue)
    {
        var httpContext = new DefaultHttpContext();

        httpContext.Request.Headers.Add("Authorization-Evaluation", headerValue);

        this.authorizationEvaluationMiddleware.InvokeAsync(httpContext);

        this.requestDelegateMock.Verify(_ => _(httpContext), Times.Once);
    }
    
    [Theory]
    [InlineData("true")]
    [InlineData("1")]
    public void Should_204Request_When_AuthorizationEvaluationHeaderTruthy(string headerValue)
    {
        var httpContext = new DefaultHttpContext();

        httpContext.Request.Headers.Add("Authorization-Evaluation", headerValue);
        
        this.authorizationEvaluationMiddleware.InvokeAsync(httpContext);
        
        Assert.Equal(204, httpContext.Response.StatusCode);
    }
    
    #endregion
}