using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Tests.Controllers
{
    [TestFixture]
    public class ProjekteControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Test] // Positive test
        public async Task GetProjekte_ReturnsAllStandorte()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Standorte.Add(new Standort {
                Name = "Test Standort 1",
                Adresse = "Adresse 1",
                Postleitzahl = "12345"
            });
            context.Standorte.Add(new Standort {
                Name = "Test Standort 2",
                Adresse = "Adresse 2",
                Postleitzahl = "67890"
            });
            await context.SaveChangesAsync();

            var controller = new ProjekteController(context);

            // Act
            var result = await controller.GetProjekte();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<Standort>>>());
            var standorte = result.Value as List<Standort>;
            Assert.That(standorte, Is.Not.Null);
            Assert.That(standorte.Count, Is.EqualTo(2));
        }

        [Test] // Positive test
        public async Task CreateProjekt_WithValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new ProjekteController(context);
            var standort = new Standort {
                Name = "Neuer Standort",
                Adresse = "Test Adresse",
                Postleitzahl = "12345"
            };

            // Act
            var result = await controller.CreateProjekt(standort);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.That(createdAtActionResult, Is.Not.Null);
            var returnValue = createdAtActionResult.Value as Standort;
            Assert.That(returnValue, Is.Not.Null);
            Assert.That(returnValue.Name, Is.EqualTo("Neuer Standort"));
            Assert.That(await context.Standorte.CountAsync(), Is.EqualTo(1));
        }

        [Test] // Negative test
        public async Task CreateProjekt_WithEmptyName_ReturnsBadRequest()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new ProjekteController(context);
            var standort = new Standort {
                Name = "",
                Adresse = "Test Adresse",
                Postleitzahl = "12345"
            };

            // Act
            var result = await controller.CreateProjekt(standort);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Projektname darf nicht leer sein."));
            Assert.That(await context.Standorte.CountAsync(), Is.EqualTo(0));
        }
    }
}