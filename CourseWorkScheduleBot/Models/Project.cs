using System.Collections.Generic;

namespace CourseWorkScheduleBot.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> Deadlines { get; set; } = new List<string>();
    }
}
