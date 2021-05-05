using CourseWorkScheduleBot.Models;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers
{
    class SetProjectDescriptionHandler : IHandler
    {
        public bool CanHandleRequest(StudentUser studentUser, Message message)
        {
            return studentUser is not null &&
                studentUser.ConversationState == ConversationState.WaitingForProjectDescription;
        }

        public Response Handle(StudentUser studentUser, Message message)
        {
            var projectDescription = message.Text;
            if (string.IsNullOrEmpty(projectDescription))
            {
                return new()
                {
                    TextMessage = new() { Text = "Помилка в описі проекту! Введіть ще раз:" },
                    ReplyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup()
                };
            }

            studentUser.Project.Description = projectDescription;
            studentUser.ConversationState = ConversationState.ProjectRegistered;

            return new()
            {
                TextMessage = new() { 
                    Text = "Дані збережено! Тепер Ви можете скористатися командами з меню!" 
                },
                ReplyMarkup = ReplyMarkupFactory.CreateDefaultKeyboardMarkup()
            };
        }
    }
}
