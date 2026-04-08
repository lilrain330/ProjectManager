using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectManagement
{
    public class ProjectManager
    {
        private const string FilePath = "projects.txt";
        public List<Project> Projects { get; private set; }

        public ProjectManager()
        {
            Projects = new List<Project>();
            LoadProjects();
        }

        public void AddProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            Projects.Add(project);
            SaveProjects();
        }

        public void RemoveProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            Projects.Remove(project);
            SaveProjects();
        }

        public void UpdateProjectProgress(Project project, int newProgress)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            project.UpdateProgress(newProgress);
            SaveProjects();
        }

        private void SaveProjects()
        {
            var lines = Projects.Select(p =>
                $"{p.Name}|{p.Description}|{p.StartDate:yyyy-MM-dd}|{p.EndDate:yyyy-MM-dd}|{p.Progress}");
            File.WriteAllLines(FilePath, lines);
        }

        private void LoadProjects()
        {
            if (!File.Exists(FilePath)) return;

            foreach (var line in File.ReadAllLines(FilePath))
            {
                var parts = line.Split('|');
                if (parts.Length != 5) continue;

                if (DateTime.TryParse(parts[2], out DateTime startDate) &&
                    DateTime.TryParse(parts[3], out DateTime endDate) &&
                    int.TryParse(parts[4], out int progress))
                {
                    var project = new Project(parts[0], parts[1], startDate, endDate);
                    project.UpdateProgress(progress);
                    Projects.Add(project);
                }
            }
        }
    }
}