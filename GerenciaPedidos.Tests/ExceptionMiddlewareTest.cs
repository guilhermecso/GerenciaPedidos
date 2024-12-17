using GerenciaPedidos.Application.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GerenciaPedidos.Tests
{
    public class ExceptionMiddlewareTest
    {
        private readonly Mock<RequestDelegate> _nextMock;
        private readonly Mock<ILogger<ExceptionMiddleware>> _loggerMock;
        private readonly ExceptionMiddleware _middleware;

        public ExceptionMiddlewareTest()
        {
            _nextMock = new Mock<RequestDelegate>();
            _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            _middleware = new ExceptionMiddleware(_nextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Test_InvokeAsync_ShouldHandleValidationException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            _nextMock.Setup(next => next(It.IsAny<HttpContext>())).Throws(new ValidationException("Validation error"));

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Test_InvokeAsync_ShouldHandleKeyNotFoundException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            _nextMock.Setup(next => next(It.IsAny<HttpContext>())).Throws(new KeyNotFoundException("Not found"));

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Test_InvokeAsync_ShouldHandleGeneralException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            _nextMock.Setup(next => next(It.IsAny<HttpContext>())).Throws(new Exception("General error"));

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
        }
    }
}
