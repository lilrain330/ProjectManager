using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProjectManagement
{
    public partial class Form1 : Form
    {
        private readonly ProjectManager _projectManager;

        public Form1()
        {
            InitializeComponent();
            _projectManager = new ProjectManager();
            RefreshList();
        }

        private void RefreshList()
        {
            projectsListBox.Items.Clear();
            foreach (var project in _projectManager.Projects)
                projectsListBox.Items.Add(project.ToString());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Дата начала должна быть раньше даты окончания!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Проект добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (projectsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите проект для удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var project = _projectManager.Projects[projectsListBox.SelectedIndex];
            try
            {
                _projectManager.RemoveProject(project);
                RefreshList();
                MessageBox.Show("Проект удалён!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateProgress_Click(object sender, EventArgs e)
        {
            if (projectsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите проект для обновления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtProgress.Text.Trim(), out int newProgress))
            {
                MessageBox.Show("Введите корректное число от 0 до 100!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var project = _projectManager.Projects[projectsListBox.SelectedIndex];
            try
            {
                _projectManager.UpdateProjectProgress(project, newProgress);
                RefreshList();
                MessageBox.Show("Прогресс обновлён!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}