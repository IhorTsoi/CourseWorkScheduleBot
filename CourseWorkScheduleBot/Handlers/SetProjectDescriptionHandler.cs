using CourseWorkScheduleBot.Handlers.Extensions;
using CourseWorkScheduleBot.Models;
using CourseWorkScheduleBot.Services;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CourseWorkScheduleBot.Handlers
{
    class SetProjectDescriptionHandler : IHandler
    {
        private readonly TelegramBotClient botClient;
        private readonly SpeechToTextService speechToTextService;

        public SetProjectDescriptionHandler(
            TelegramBotClient botClient, SpeechToTextService speechToTextService)
        {
            this.botClient = botClient;
            this.speechToTextService = speechToTextService;
        }

        public bool CanHandleRequest(StudentUser studentUser, Message message)
        {
            return studentUser is not null &&
                studentUser.ConversationState == ConversationState.WaitingForProjectDescription;
        }

        public async Task<Response> HandleAsync(StudentUser studentUser, Message message)
        {
            var projectDescription = await ExtractProjectDescriptionAsync(message);
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
                TextMessage = new()
                {
                    Text = $"Дякую, було встановлено опис '{projectDescription}'! " +
                            $"Тепер Ви можете скористатися командами з меню!"
                },
                ReplyMarkup = ReplyMarkupFactory.CreateDefaultKeyboardMarkup()
            };
        }

        private async Task<string> ExtractProjectDescriptionAsync(Message message) =>
            message.Type switch
            {
                MessageType.Text => message.Text,
                MessageType.Voice => await botClient.RecognizeSpeechFromVoiceMessageAsync(
                    message, speechToTextService),
                _ => throw new Exception("Unsupported message type while extracting project description."),
            };
    }
}
