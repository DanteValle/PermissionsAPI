using Moq;
using PermissionsAPI.CQRS.Commands;
using PermissionsAPI.Model;
using PermissionsAPI.Repositories;
using PermissionsAPI.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PermissionsAPI.Test
{
    public class PermissionServiceTests
    {
        [Fact]
        public async Task RequestPermissionAsync_ShouldReturnNewPermission()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockElasticSearchService = new Mock<IElasticSearchService>();

            // Configurar el repositorio para agregar el permiso
            mockUnitOfWork.Setup(u => u.PermissionRepository.AddAsync(It.IsAny<Permission>()))
                          .Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.SaveAsync())
                          .ReturnsAsync(1);

            var service = new PermissionService(mockUnitOfWork.Object, mockElasticSearchService.Object);
            var command = new RequestPermissionCommand { Id = 1, PermissionTypeId = 2 };

            // Act
            var result = await service.RequestPermissionAsync(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
            Assert.Equal(command.PermissionTypeId, result.PermissionTypeId);
            Assert.Equal(command.NameEmploye, result.NameEmployee);
            // Verificar que se llame a Elasticsearch
            mockElasticSearchService.Verify(es => es.IndexPermissionAsync(It.IsAny<Permission>()), Times.Once);
        }
    }
}
