using Backend.Controllers;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Tests.Controllers
{
    [TestFixture]
    public class HealthControllerTests
    {
        [Test] // Positive test
        public async Task CheckDatabaseConnection_WhenConnectionSuccessful_ReturnsOk()
        {
            // Arrange
            var mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            var mockDatabase = new Mock<DatabaseFacade>(mockContext.Object);

            mockContext.Setup(c => c.Database).Returns(mockDatabase.Object);
            mockDatabase.Setup(d => d.CanConnectAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var controller = new HealthController(mockContext.Object);

            // Act
            var result = await controller.CheckDatabaseConnection();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            dynamic response = okResult.Value;
            Assert.That(response.Status.ToString(), Is.EqualTo("Connected"));
        }

        [Test] // Negative test
        public async Task CheckDatabaseConnection_WhenConnectionFails_ReturnsInternalServerError()
        {
            // Arrange
            var mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            var mockDatabase = new Mock<DatabaseFacade>(mockContext.Object);

            mockContext.Setup(c => c.Database).Returns(mockDatabase.Object);
            mockDatabase.Setup(d => d.CanConnectAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var controller = new HealthController(mockContext.Object);

            // Act
            var result = await controller.CheckDatabaseConnection();

            // Assert
            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var statusCodeResult = result as ObjectResult;
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
            dynamic response = statusCodeResult.Value;
            Assert.That(response.Status.ToString(), Is.EqualTo("Error"));
        }
    }
}