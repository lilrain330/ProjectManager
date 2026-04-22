using System;

namespace ProjectManagement
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Progress { get; private set; }

        public Project(string name, string description, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Progress = 0;
        }

        public void UpdateProgress(int newProgress)
        {
            if (newProgress < 0 || newProgress > 100)
                throw new ArgumentOutOfRangeException(nameof(newProgress),
                    "Прогресс должен быть между 0 и 100.");
            Progress = newProgress;
        }

        public override string ToString()
        {
            return $"{Name} — Прогресс: {Progress}%";
        }
    }
}