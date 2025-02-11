using Microsoft.AspNetCore.Mvc;
using Moq;
using PermissionsAPI.Controllers;
using PermissionsAPI.CQRS.Commands;
using PermissionsAPI.CQRS.Queries;
using PermissionsAPI.Infrastructure;
using PermissionsAPI.Model;
using PermissionsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsApi.Tests
{
    public class PermissionsTests
    {
        private readonly Mock<IPermissionService> _permissionServiceMock;
        private readonly Mock<ILog> _logMock;
        private readonly PermissionsController _controller;

        public PermissionsTests()
        {
            _permissionServiceMock = new Mock<IPermissionService>();
            _logMock = new Mock<ILog>();
            _controller = new PermissionsController(_permissionServiceMock.Object, _logMock.Object);
        }

        [Fact]
        public async Task RequestPermission_ReturnsOkResult_WithExpectedPermission()
        {
            // Arrange: Definir el comando y la respuesta esperada.
            var command = new RequestPermissionCommand
            {
                PermissionTypeId = 1,
                NameEmploye = "John",
                LastNameEmployee = "Doe"
            };

            var expectedPermission = new Permission
            {
                PermissionTypeId = command.PermissionTypeId,
                NameEmployee = command.NameEmploye,
                LastNameEmployee = command.LastNameEmployee,
                PermissionDate = DateTime.UtcNow
            };

            _permissionServiceMock
                .Setup(x => x.RequestPermissionAsync(command))
                .ReturnsAsync(expectedPermission);

            // Act: Llamar al endpoint.
            var result = await _controller.RequestPermission(command);

            // Assert: Verificar que se retorne un OkObjectResult con el objeto Permission esperado.
            var okResult = Assert.IsType<OkObjectResult>(result);
            var permissionResult = Assert.IsType<Permission>(okResult.Value);

            Assert.Equal(expectedPermission.PermissionTypeId, permissionResult.PermissionTypeId);
            Assert.Equal(expectedPermission.NameEmployee, permissionResult.NameEmployee);
            Assert.Equal(expectedPermission.LastNameEmployee, permissionResult.LastNameEmployee);
        }

        [Fact]
        public async Task RequestPermission_WhenExceptionThrown_ThrowsExceptionAndLogsError()
        {
            // Arrange
            var command = new RequestPermissionCommand
            {
                PermissionTypeId = 1,
                NameEmploye = "John",
                LastNameEmployee = "Doe"
            };

            var exception = new Exception("Test exception");
            _permissionServiceMock
                .Setup(x => x.RequestPermissionAsync(command))
                .ThrowsAsync(exception);

            // Act & Assert: Se espera que se lance la excepción.
            var ex = await Assert.ThrowsAsync<Exception>(() => _controller.RequestPermission(command));
            Assert.Equal("Test exception", ex.Message);

            // Verificar que se haya llamado el log con el mensaje correspondiente.
            _logMock.Verify(x => x.Exeption("RequestPermission", exception), Times.Once);
        }

        [Fact]
        public async Task ModifyPermission_ReturnsOkResult_WithExpectedPermission()
        {
            // Arrange: Configurar el comando para modificar y la respuesta esperada.
            var command = new ModifyPermissionCommand
            {
                PermissionId = 1,
                PermissionTypeId = 2,
                NameEmploye = "Jane",
                LastNameEmployee = "Doe"
            };

            var expectedPermission = new Permission
            {
                PermissionTypeId = command.PermissionTypeId,
                NameEmployee = command.NameEmploye,
                LastNameEmployee = command.LastNameEmployee,
                PermissionDate = DateTime.UtcNow
            };

            _permissionServiceMock
                .Setup(x => x.ModifyPermissionAsync(command))
                .ReturnsAsync(expectedPermission);

            // Act
            var result = await _controller.ModifyPermission(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var permissionResult = Assert.IsType<Permission>(okResult.Value);

            Assert.Equal(expectedPermission.PermissionTypeId, permissionResult.PermissionTypeId);
            Assert.Equal(expectedPermission.NameEmployee, permissionResult.NameEmployee);
            Assert.Equal(expectedPermission.LastNameEmployee, permissionResult.LastNameEmployee);
        }

        [Fact]
        public async Task ModifyPermission_WhenExceptionThrown_ThrowsExceptionAndLogsError()
        {
            // Arrange
            var command = new ModifyPermissionCommand
            {
                PermissionId = 1,
                PermissionTypeId = 2,
                NameEmploye = "Jane",
                LastNameEmployee = "Doe"
            };

            var exception = new Exception("Test exception");
            _permissionServiceMock
                .Setup(x => x.ModifyPermissionAsync(command))
                .ThrowsAsync(exception);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _controller.ModifyPermission(command));
            Assert.Equal("Test exception", ex.Message);
            _logMock.Verify(x => x.Exeption("ModifyPermission", exception), Times.Once);
        }

        [Fact]
        public async Task GetPermissions_ReturnsOkResult_WithListOfPermissions()
        {
            // Arrange: Configurar una lista de permisos de ejemplo.
            var expectedPermissions = new List<Permission>
            {
                new Permission
                {
                    PermissionTypeId = 1,
                    NameEmployee = "John",
                    LastNameEmployee = "Doe",
                    PermissionDate = DateTime.UtcNow
                },
                new Permission
                {
                    PermissionTypeId = 2,
                    NameEmployee = "Jane",
                    LastNameEmployee = "Doe",
                    PermissionDate = DateTime.UtcNow
                }
            };

            _permissionServiceMock
                .Setup(x => x.GetPermissionsAsync(It.IsAny<GetPermissionsQuery>()))
                .ReturnsAsync(expectedPermissions);

            // Act
            var result = await _controller.GetPermissions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var permissionsResult = Assert.IsAssignableFrom<IEnumerable<Permission>>(okResult.Value);

            // Para comparar la cantidad de elementos.
            Assert.Equal(expectedPermissions.Count, ((List<Permission>)permissionsResult).Count);
        }

        [Fact]
        public async Task GetPermissions_WhenExceptionThrown_ThrowsExceptionAndLogsError()
        {
            // Arrange
            var exception = new Exception("Test exception");
            _permissionServiceMock
                .Setup(x => x.GetPermissionsAsync(It.IsAny<GetPermissionsQuery>()))
                .ThrowsAsync(exception);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _controller.GetPermissions());
            Assert.Equal("Test exception", ex.Message);
            _logMock.Verify(x => x.Exeption("GetPermissions", exception), Times.Once);
        }
    }
}
