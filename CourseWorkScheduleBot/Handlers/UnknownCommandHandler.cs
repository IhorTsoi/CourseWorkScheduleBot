using CourseWorkScheduleBot.Models;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers
{
    class UnknownCommandHandler : IHandler
    {
        public bool CanHandleRequest(StudentUser studentUser, Message message) => true;

        public Response Handle(StudentUser studentUser, Message message)
        {
            var text = "Нажаль, я Вас не розумію😞";
            var replyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup();

            if (studentUser is not null &&
                studentUser.ConversationState == ConversationState.ProjectRegistered)
            {
                text = $"{text} Будь-ласка, оберіть одну з команд!";
                replyMarkup = ReplyMarkupFactory.CreateDefaultKeyboardMarkup();
            }

            return new()
            {
                TextMessage = new() { Text =  text },
                ReplyMarkup = replyMarkup
            };
        }
    }
}
