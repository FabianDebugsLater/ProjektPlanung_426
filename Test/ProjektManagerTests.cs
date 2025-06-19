using System;
using Xunit;
using Projektplanung_426;

namespace Projektplanung_426.Test
{
    public class ProjektManagerTests
    {
        [Fact]
        public void Hinzufügen_EinProjekt_PositiveTest()
        {
            // Arrange
            var manager = new ProjektManager();

            // Act
            manager.Hinzufügen("Projekt A");
            var projekte = manager.AlleAnzeigen();

            // Assert
            Assert.Single(projekte);
            Assert.Contains("Projekt A", projekte);
        }

        [Fact]
        public void AlleAnzeigen_MehrereProjekte_PositiveTest()
        {
            // Arrange
            var manager = new ProjektManager();

            // Act
            manager.Hinzufügen("Projekt A");
            manager.Hinzufügen("Projekt B");
            var projekte = manager.AlleAnzeigen();

            // Assert
            Assert.Equal(2, projekte.Count);
            Assert.Contains("Projekt A", projekte);
            Assert.Contains("Projekt B", projekte);
        }

        [Fact]
        public void Hinzufügen_NullOderLeer_NegativeTest()
        {
            // Arrange
            var manager = new ProjektManager();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => manager.Hinzufügen(null));
            Assert.Throws<ArgumentException>(() => manager.Hinzufügen(""));
            Assert.Throws<ArgumentException>(() => manager.Hinzufügen("   "));
        }

        [Fact]
        public void Hinzufügen_Duplikat_NegativeTest()
        {
            // Arrange
            var manager = new ProjektManager();
            manager.Hinzufügen("Projekt A");

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => manager.Hinzufügen("Projekt A"));
            Assert.Equal("Projekt existiert bereits.", ex.Message);
        }
    }
}
