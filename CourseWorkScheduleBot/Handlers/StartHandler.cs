using CourseWorkScheduleBot.Models;
using CourseWorkScheduleBot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CourseWorkScheduleBot.Handlers
{
    class StartHandler : IHandler
    {
        private readonly LocalStorage localStorage;

        public StartHandler(LocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public bool CanHandleRequest(StudentUser studentUser, Message message)
        {
            return
                studentUser is null &&
                message.Type == MessageType.Text &&
                message.Text.Equals(Commands.Start);
        }

        public Response Handle(StudentUser studentUser, Message message)
        {
            var userId = message.From.Id;
            localStorage.AddUser(new(userId));

            return new()
            {
                TextMessage = new()
                {
                    Text =
                    "Вітаю☺️ Вас було зареєстровано!\n" +
                    "Будь-ласка, введіть назву Вашого проекту:"
                },
                ReplyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup()
            };
        }
    }
}
