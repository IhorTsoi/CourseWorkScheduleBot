using CourseWorkScheduleBot.Models;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers
{
    class UnknownCommandHandler : IHandler
    {
        public bool CanHandleRequest(StudentUser studentUser, Message message) => true;

        public Task<Response> HandleAsync(StudentUser studentUser, Message message)
        {
            var text = "Нажаль, я Вас не розумію😞";
            var replyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup();

            if (studentUser is not null &&
                studentUser.ConversationState == ConversationState.ProjectRegistered)
            {
                text = $"{text} Будь-ласка, оберіть одну з команд!";
                replyMarkup = ReplyMarkupFactory.CreateDefaultKeyboardMarkup();
            }

            return Task.FromResult(new Response()
            {
                TextMessage = new() { Text = text },
                ReplyMarkup = replyMarkup
            });
        }
    }
}
