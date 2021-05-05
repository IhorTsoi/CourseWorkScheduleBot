using CourseWorkScheduleBot.Models;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers
{
    class SetProjectNameHandler : IHandler
    {
        public bool CanHandleRequest(StudentUser studentUser, Message message)
        {
            return studentUser is not null &&
                studentUser.ConversationState == ConversationState.WaitingForProjectName;
        }

        public Response Handle(StudentUser studentUser, Message message)
        {
            var projectName = message.Text;
            if (string.IsNullOrEmpty(projectName))
            {
                return new()
                {
                    TextMessage = new() { Text = "Помилка у назві проекту! Введіть ще раз:" },
                    ReplyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup()
                };
            }

            studentUser.Project.Name = projectName;
            studentUser.ConversationState = ConversationState.WaitingForProjectDescription;

            return new()
            {
                TextMessage = new() { Text = "Добре! Будь-ласка, введіть короткий опис проекту:" },
                ReplyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup()
            };
        }
    }
}
