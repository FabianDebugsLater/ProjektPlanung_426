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
        public async Task GetProjekte_ReturnsAllProjekte()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Projekte.Add(new Projekt {
                Name = "Test Projekt 1",
                Beschreibung = "Beschreibung 1",
                Status = "ToDo",
                KundeId = 1
            });
            context.Projekte.Add(new Projekt {
                Name = "Test Projekt 2",
                Beschreibung = "Beschreibung 2",
                Status = "InProgress",
                KundeId = 2
            });
            await context.SaveChangesAsync();

            var controller = new ProjekteController(context);

            // Act
            var result = await controller.GetProjekte();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<Projekt>>>());
            var projekte = result.Value as List<Projekt>;
            Assert.That(projekte, Is.Not.Null);
            Assert.That(projekte.Count, Is.EqualTo(2));
        }

        [Test] // Positive test
        public async Task CreateProjekt_WithValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new ProjekteController(context);
            var projekt = new Projekt {
                Name = "Neues Projekt",
                Beschreibung = "Test Beschreibung",
                Status = "ToDo",
                KundeId = 1
            };

            // Act
            var result = await controller.CreateProjekt(projekt);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.That(createdAtActionResult, Is.Not.Null);
            var returnValue = createdAtActionResult.Value as Projekt;
            Assert.That(returnValue, Is.Not.Null);
            Assert.That(returnValue.Name, Is.EqualTo("Neues Projekt"));
            Assert.That(await context.Projekte.CountAsync(), Is.EqualTo(1));
        }

        [Test] // Negative test
        public async Task CreateProjekt_WithEmptyName_ReturnsBadRequest()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new ProjekteController(context);
            var projekt = new Projekt {
                Name = "",
                Beschreibung = "Test Beschreibung",
                Status = "ToDo",
                KundeId = 1
            };

            // Act
            var result = await controller.CreateProjekt(projekt);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Projektname darf nicht leer sein."));
            Assert.That(await context.Projekte.CountAsync(), Is.EqualTo(0));
        }
    }
}