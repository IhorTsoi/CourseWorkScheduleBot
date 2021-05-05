using CourseWorkScheduleBot.Models;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers
{
    class UnknownCommandHandler : IHandler
    {
        public bool CanHandleRequest(StudentUser studentUser, Message message) => true;

        public Response Handle(StudentUser studentUser, Message message)
        {
            var replyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup();
            if (studentUser is not null &&
                studentUser.ConversationState == ConversationState.ProjectRegistered)
            {
                replyMarkup = ReplyMarkupFactory.CreateDefaultKeyboardMarkup();
            }

            return new()
            {
                TextMessage = new() { Text = "Я Вас не розумію(" },
                ReplyMarkup = replyMarkup
            };
        }
    }
}
