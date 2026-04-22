using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;
using ProjectManagement;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class Form1LogicTests
    {
        private Form1 _form;

        [TestInitialize]
        public void SetUp()
        {
            if (File.Exists("projects.txt"))
                File.Delete("projects.txt");

            _form = new Form1();
            _form.SuppressMessages = true; // Блокируем MessageBox во всех тестах
        }

        [TestCleanup]
        public void TearDown()
        {
            _form?.Dispose();

            if (File.Exists("projects.txt"))
                File.Delete("projects.txt");
        }

        // ───────────── Вспомогательный метод ─────────────

        private void AddProject(string name, string description,
            DateTime start, DateTime end)
        {
            _form.txtName.Text = name;
            _form.txtDescription.Text = description;
            _form.dtpStart.Value = start;
            _form.dtpEnd.Value = end;
            _form.btnAdd_Click(null, EventArgs.Empty);
        }

        // ───────────── Кнопка «Добавить» ─────────────

        [TestMethod]
        public void BtnAdd_ValidData_AddsItemToListBox()
        {
            // Act
            AddProject("Новый проект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Assert
            Assert.AreEqual(1, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnAdd_EmptyName_DoesNotAddProject()
        {
            // Act
            AddProject("", "Описание",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Assert
            Assert.AreEqual(0, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnAdd_EmptyDescription_DoesNotAddProject()
        {
            // Act
            AddProject("Проект", "",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Assert
            Assert.AreEqual(0, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnAdd_StartAfterEnd_DoesNotAddProject()
        {
            // Act — конец раньше начала
            AddProject("Проект", "Описание",
                DateTime.Today.AddDays(10), DateTime.Today);

            // Assert
            Assert.AreEqual(0, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnAdd_AfterAdd_ClearsNameField()
        {
            // Act
            AddProject("Проект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));

            // Assert
            Assert.AreEqual(string.Empty, _form.txtName.Text);
        }

        [TestMethod]
        public void BtnAdd_AfterAdd_ClearsDescriptionField()
        {
            // Act
            AddProject("Проект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));

            // Assert
            Assert.AreEqual(string.Empty, _form.txtDescription.Text);
        }

        [TestMethod]
        public void BtnAdd_TwoProjects_ListBoxHasTwoItems()
        {
            // Act
            AddProject("Проект 1", "Описание 1",
                DateTime.Today, DateTime.Today.AddDays(5));
            AddProject("Проект 2", "Описание 2",
                DateTime.Today, DateTime.Today.AddDays(10));

            // Assert
            Assert.AreEqual(2, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnRemove_WithSelectedProject_RemovesFromListBox()
        {
            // Arrange
            AddProject("Удалить меня", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            _form.projectsListBox.SelectedIndex = 0;

            // Act
            _form.btnRemove_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual(0, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnRemove_NoSelection_ListBoxUnchanged()
        {
            // Arrange
            AddProject("Проект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            _form.projectsListBox.SelectedIndex = -1;

            // Act
            _form.btnRemove_Click(null, EventArgs.Empty);

            // Assert — проект не удалён
            Assert.AreEqual(1, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnRemove_OneOfTwo_OneRemains()
        {
            // Arrange
            AddProject("Проект 1", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            AddProject("Проект 2", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            _form.projectsListBox.SelectedIndex = 0;

            // Act
            _form.btnRemove_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual(1, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnUpdateProgress_ValidValue_UpdatesListBox()
        {
            // Arrange
            AddProject("Проект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            _form.projectsListBox.SelectedIndex = 0;
            _form.txtProgress.Text = "75";

            // Act
            _form.btnUpdateProgress_Click(null, EventArgs.Empty);

            // Assert
            StringAssert.Contains(
                _form.projectsListBox.Items[0].ToString(), "75");
        }

        [TestMethod]
        public void BtnUpdateProgress_NoSelection_DoesNotCrash()
        {
            // Arrange
            _form.projectsListBox.SelectedIndex = -1;
            _form.txtProgress.Text = "50";

            // Act — не должно бросить исключение
            _form.btnUpdateProgress_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual(0, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void BtnUpdateProgress_InvalidText_DoesNotCrash()
        {
            // Arrange
            AddProject("Проект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            _form.projectsListBox.SelectedIndex = 0;
            _form.txtProgress.Text = "abc";

            // Act — не должно бросить исключение
            _form.btnUpdateProgress_Click(null, EventArgs.Empty);

            // Assert — прогресс не изменился, остался 0%
            StringAssert.Contains(
                _form.projectsListBox.Items[0].ToString(), "0");
        }

        [TestMethod]
        public void BtnUpdateProgress_ZeroValue_SetsZeroPercent()
        {
            // Arrange
            AddProject("Проект", "Описание",
                DateTime.Today, DateTime.Today.AddDays(5));
            _form.projectsListBox.SelectedIndex = 0;
            _form.txtProgress.Text = "0";

            // Act
            _form.btnUpdateProgress_Click(null, EventArgs.Empty);

            // Assert
            StringAssert.Contains(
                _form.projectsListBox.Items[0].ToString(), "0");
        }
    }
}