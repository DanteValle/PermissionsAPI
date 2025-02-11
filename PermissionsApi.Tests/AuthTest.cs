using Microsoft.AspNetCore.Mvc;
using Moq;
using PermissionsAPI.Controllers;
using PermissionsAPI.Infrastructure;
using PermissionsAPI.Model;
using PermissionsAPI.Services;

namespace PermissionsApi.Tests
{
    public class AuthTest
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ILog> _logMock;
        private readonly AuthController _controller;

        public AuthTest()
        {
            _authServiceMock = new Mock<IAuthService>();
            _logMock = new Mock<ILog>();
            _controller = new AuthController(_authServiceMock.Object, _logMock.Object);
        }

        [Fact]
        public void Login_ConCredencialesValidas_RetornaOkConToken()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Username = "user",
                Password = "password"
            };
            string expectedToken = "dummy-token";
            _authServiceMock
                .Setup(x => x.GenerateToken(loginRequest.Username))
                .Returns(expectedToken);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Usar reflection para acceder a la propiedad 'token'
            var value = okResult.Value;
            var propertyInfo = value.GetType().GetProperty("token");
            Assert.NotNull(propertyInfo); // Verificamos que la propiedad exista
            var tokenValue = propertyInfo.GetValue(value, null) as string;

            Assert.Equal(expectedToken, tokenValue);
        }

        [Fact]
        public void Login_ConCredencialesInvalidas_RetornaUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Username = "usuarioIncorrecto",
                Password = "claveIncorrecta"
            };

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Credenciales inválidas", unauthorizedResult.Value);
        }

        [Fact]
        public void Login_CuandoOcurreExcepcion_PropagaExcepcionYRegistraError()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Username = "user",
                Password = "password"
            };
            var exception = new Exception("Error de prueba");
            _authServiceMock
                .Setup(x => x.GenerateToken(loginRequest.Username))
                .Throws(exception);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _controller.Login(loginRequest));
            Assert.Equal("Error de prueba", ex.Message);
            _logMock.Verify(x => x.Error(exception, ""), Times.Once);
        }
    }
}