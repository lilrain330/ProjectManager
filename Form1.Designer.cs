namespace ProjectManagement
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.TextBox txtProgress;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnUpdateProgress;
        private System.Windows.Forms.ListBox projectsListBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.txtProgress = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUpdateProgress = new System.Windows.Forms.Button();
            this.projectsListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Text = "Название:";

            // txtName
            this.txtName.Location = new System.Drawing.Point(100, 12);
            this.txtName.Width = 200;

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 48);
            this.lblDescription.Text = "Описание:";

            // txtDescription
            this.txtDescription.Location = new System.Drawing.Point(100, 45);
            this.txtDescription.Width = 370;

            // lblStart
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(12, 82);
            this.lblStart.Text = "Дата начала:";

            // dtpStart
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(100, 79);
            this.dtpStart.Width = 120;

            // lblEnd
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(240, 82);
            this.lblEnd.Text = "Дата конца:";

            // dtpEnd
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(330, 79);
            this.dtpEnd.Width = 120;

            // lblProgress
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 116);
            this.lblProgress.Text = "Прогресс (0–100):";

            // txtProgress
            this.txtProgress.Location = new System.Drawing.Point(140, 113);
            this.txtProgress.Width = 60;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(12, 148);
            this.btnAdd.Size = new System.Drawing.Size(110, 30);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnRemove
            this.btnRemove.Location = new System.Drawing.Point(132, 148);
            this.btnRemove.Size = new System.Drawing.Size(110, 30);
            this.btnRemove.Text = "Удалить";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);

            // btnUpdateProgress
            this.btnUpdateProgress.Location = new System.Drawing.Point(252, 148);
            this.btnUpdateProgress.Size = new System.Drawing.Size(160, 30);
            this.btnUpdateProgress.Text = "Обновить прогресс";
            this.btnUpdateProgress.Click += new System.EventHandler(this.btnUpdateProgress_Click);

            // projectsListBox
            this.projectsListBox.Location = new System.Drawing.Point(12, 195);
            this.projectsListBox.Size = new System.Drawing.Size(560, 220);
            this.projectsListBox.Font = new System.Drawing.Font("Segoe UI", 10f);

            // Form1
            this.ClientSize = new System.Drawing.Size(590, 435);
            this.Text = "Управление проектами";
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.txtProgress);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnUpdateProgress);
            this.Controls.Add(this.projectsListBox);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}