using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;
using ProjectManagement;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class UITests
    {
        private Form1 _form;

        [TestInitialize]
        public void SetUp()
        {
            if (File.Exists("projects.txt"))
                File.Delete("projects.txt");

            _form = new Form1();
        }

        [TestCleanup]
        public void TearDown()
        {
            _form?.Dispose();

            if (File.Exists("projects.txt"))
                File.Delete("projects.txt");
        }

        [TestMethod]
        public void Form_OnLoad_ListBoxIsEmpty()
        {
            Assert.AreEqual(0, _form.projectsListBox.Items.Count);
        }

        [TestMethod]
        public void Form_OnLoad_TxtNameIsEmpty()
        {
            Assert.AreEqual(string.Empty, _form.txtName.Text);
        }

        [TestMethod]
        public void Form_OnLoad_TxtDescriptionIsEmpty()
        {
            Assert.AreEqual(string.Empty, _form.txtDescription.Text);
        }

        [TestMethod]
        public void Form_Title_IsCorrect()
        {
            StringAssert.Contains(_form.Text, "проект");
        }
    }
}