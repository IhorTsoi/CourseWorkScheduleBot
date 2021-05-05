using CourseWorkScheduleBot.Models;
using System.Collections.Generic;
using System.Linq;

namespace CourseWorkScheduleBot.Storage
{
    public class LocalStorage
    {
        private readonly List<StudentUser> users = new();

        public bool IncludesUserWithId(int id) => users.Any(u => id == u.Id);

        public void AddUser(StudentUser user) => users.Add(user);

        public StudentUser GetUserById(int id) => users.First(u => id == u.Id);
    }
}
