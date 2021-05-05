namespace CourseWorkScheduleBot.Models
{
    public class StudentUser
    {
        public StudentUser(int id)
        {
            Id = id;
            ConversationState = ConversationState.WaitingForProjectName;
            Project = new();
        }

        public int Id { get; }
        public ConversationState ConversationState { get; set; }
        public Project Project { get; set; }
    }
}
