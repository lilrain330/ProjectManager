using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ProjectManagement;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class ProjectManagerTests
    {
        private ProjectManager _manager;
        private const string TestFile = "projects.txt";

        [TestInitialize]
        public void SetUp()
        {
            // Удаляем файл перед каждым тестом для чистоты
            if (File.Exists(TestFile))
                File.Delete(TestFile);

            _manager = new ProjectManager();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (File.Exists(TestFile))
                File.Delete(TestFile);
        }

        [TestMethod]
        public void AddProject_ValidProject_IncreasesCount()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Act
            _manager.AddProject(project);

            // Assert
            Assert.AreEqual(1, _manager.Projects.Count);
        }

        [TestMethod]
        public void AddProject_TwoProjects_CountIsTwo()
        {
            // Arrange
            var p1 = new Project("Проект 1", "Описание 1",
                DateTime.Today, DateTime.Today.AddDays(5));
            var p2 = new Project("Проект 2", "Описание 2",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Act
            _manager.AddProject(p1);
            _manager.AddProject(p2);

            // Assert
            Assert.AreEqual(2, _manager.Projects.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProject_NullProject_ThrowsException()
        {
            // Act
            _manager.AddProject(null);
        }

        [TestMethod]
        public void AddProject_SavesToFile()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Act
            _manager.AddProject(project);

            // Assert
            Assert.IsTrue(File.Exists(TestFile));
        }

        [TestMethod]
        public void RemoveProject_ExistingProject_DecreasesCount()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));
            _manager.AddProject(project);

            // Act
            _manager.RemoveProject(project);

            // Assert
            Assert.AreEqual(0, _manager.Projects.Count);
        }

        [TestMethod]
        public void RemoveProject_ExistingProject_ProjectNotInList()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));
            _manager.AddProject(project);

            // Act
            _manager.RemoveProject(project);

            // Assert
            Assert.IsFalse(_manager.Projects.Contains(project));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveProject_NullProject_ThrowsException()
        {
            // Act
            _manager.RemoveProject(null);
        }

        [TestMethod]
        public void RemoveProject_OneOfTwo_CountIsOne()
        {
            // Arrange
            var p1 = new Project("Проект 1", "Описание 1",
                DateTime.Today, DateTime.Today.AddDays(5));
            var p2 = new Project("Проект 2", "Описание 2",
                DateTime.Today, DateTime.Today.AddDays(10));
            _manager.AddProject(p1);
            _manager.AddProject(p2);

            // Act
            _manager.RemoveProject(p1);

            // Assert
            Assert.AreEqual(1, _manager.Projects.Count);
            Assert.IsTrue(_manager.Projects.Contains(p2));
        }


        [TestMethod]
        public void UpdateProjectProgress_ValidValue_UpdatesProgress()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));
            _manager.AddProject(project);

            // Act
            _manager.UpdateProjectProgress(project, 60);

            // Assert
            Assert.AreEqual(60, project.Progress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateProjectProgress_NullProject_ThrowsException()
        {
            // Act
            _manager.UpdateProjectProgress(null, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateProjectProgress_NegativeValue_ThrowsException()
        {
            // Arrange
            var project = new Project("Тест", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));
            _manager.AddProject(project);

            // Act
            _manager.UpdateProjectProgress(project, -5);
        }

        [TestMethod]
        public void SaveAndLoad_ProjectIsSavedAndRestored()
        {
            // Arrange
            var start = new DateTime(2024, 3, 1);
            var end = new DateTime(2024, 6, 30);
            var project = new Project("Сохранённый", "Описание", start, end);
            project.UpdateProgress(35);
            _manager.AddProject(project);

            // Act — создаём новый менеджер, он читает из файла
            var manager2 = new ProjectManager();

            // Assert
            Assert.AreEqual(1, manager2.Projects.Count);
            Assert.AreEqual("Сохранённый", manager2.Projects[0].Name);
            Assert.AreEqual(35, manager2.Projects[0].Progress);
        }

        [TestMethod]
        public void Load_NoFile_EmptyList()
        {
            // Assert — файл удалён в SetUp, список должен быть пустым
            Assert.AreEqual(0, _manager.Projects.Count);
        }
    }
}