using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectManagement;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class ProjectTests
    {

        [TestMethod]
        public void Constructor_ValidData_CreatesProject()
        {
            // Arrange
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 12, 31);

            // Act
            var project = new Project("Проект А", "Описание А", start, end);

            // Assert
            Assert.AreEqual("Проект А", project.Name);
            Assert.AreEqual("Описание А", project.Description);
            Assert.AreEqual(start, project.StartDate);
            Assert.AreEqual(end, project.EndDate);
            Assert.AreEqual(0, project.Progress);
        }

        [TestMethod]
        public void Constructor_SetsProgressToZeroByDefault()
        {
            // Arrange & Act
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Assert
            Assert.AreEqual(0, project.Progress);
        }

        [TestMethod]
        public void UpdateProgress_ValidValue_UpdatesProgress()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Act
            project.UpdateProgress(50);

            // Assert
            Assert.AreEqual(50, project.Progress);
        }

        [TestMethod]
        public void UpdateProgress_ZeroValue_SetsProgressToZero()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));
            project.UpdateProgress(40);

            // Act
            project.UpdateProgress(0);

            // Assert
            Assert.AreEqual(0, project.Progress);
        }

        [TestMethod]
        public void UpdateProgress_HundredValue_SetsProgressToHundred()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Act
            project.UpdateProgress(100);

            // Assert
            Assert.AreEqual(100, project.Progress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateProgress_NegativeValue_ThrowsException()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Act
            project.UpdateProgress(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateProgress_ValueAbove100_ThrowsException()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Act
            project.UpdateProgress(101);
        }

        [TestMethod]
        public void ToString_ContainsNameAndProgress()
        {
            // Arrange
            var project = new Project("МойПроект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            project.UpdateProgress(75);

            // Act
            var result = project.ToString();

            // Assert
            StringAssert.Contains(result, "МойПроект");
            StringAssert.Contains(result, "75");
        }

        [TestMethod]
        public void ToString_NewProject_ShowsZeroProgress()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));

            // Act
            var result = project.ToString();

            // Assert
            StringAssert.Contains(result, "0");
        }
    }
}