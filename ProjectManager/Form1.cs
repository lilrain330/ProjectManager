using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProjectManagement
{
    public partial class Form1 : Form
    {
        public readonly ProjectManager _projectManager;
        public bool SuppressMessages { get; set; } = false;

        public Form1()
        {
            InitializeComponent();
            _projectManager = new ProjectManager();
            RefreshList();
        }

        public void RefreshList()
        {
            projectsListBox.Items.Clear();
            foreach (var project in _projectManager.Projects)
                projectsListBox.Items.Add(project.ToString());
        }

        public void ShowMessage(string text, string caption, MessageBoxIcon icon)
        {
            if (!SuppressMessages)
                MessageBox.Show(text, caption, MessageBoxButtons.OK, icon);
        }
        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                ShowMessage("Заполните все поля!", "Ошибка", MessageBoxIcon.Error);
                return;
            }

            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date;

            if (startDate > endDate)
            {
                ShowMessage("Дата начала должна быть раньше даты окончания!",
                    "Ошибка", MessageBoxIcon.Error);
                return;
            }

            try
            {
                var project = new Project(
                    txtName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    startDate,
                    endDate);

                _projectManager.AddProject(project);
                txtName.Clear();
                txtDescription.Clear();
                RefreshList();
                ShowMessage("Проект добавлен!", "Успех", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Ошибка", MessageBoxIcon.Error);
            }
        }

        public void btnRemove_Click(object sender, EventArgs e)
        {
            if (projectsListBox.SelectedIndex == -1)
            {
                ShowMessage("Выберите проект для удаления!", "Ошибка",
                    MessageBoxIcon.Error);
                return;
            }

            var project = _projectManager.Projects[projectsListBox.SelectedIndex];
            try
            {
                _projectManager.RemoveProject(project);
                RefreshList();
                ShowMessage("Проект удалён!", "Успех", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Ошибка", MessageBoxIcon.Error);
            }
        }

        public void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            if (projectsListBox.SelectedIndex == -1)
            {
                ShowMessage("Выберите проект для обновления!", "Ошибка",
                    MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtProgress.Text.Trim(), out int newProgress) ||
                newProgress < 0 || newProgress > 100)
            {
                ShowMessage("Введите корректное число от 0 до 100!", "Ошибка",
                    MessageBoxIcon.Error);
                return;
            }

            var project = _projectManager.Projects[projectsListBox.SelectedIndex];
            try
            {
                _projectManager.UpdateProjectProgress(project, newProgress);
                RefreshList();
                ShowMessage("Прогресс обновлён!", "Успех", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Ошибка", MessageBoxIcon.Error);
            }
        }
    }
}