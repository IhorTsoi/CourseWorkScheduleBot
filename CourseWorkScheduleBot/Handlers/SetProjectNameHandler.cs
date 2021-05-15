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
    class SetProjectNameHandler : IHandler
    {
        private readonly TelegramBotClient botClient;
        private readonly SpeechToTextService speechToTextService;

        public SetProjectNameHandler(
            TelegramBotClient botClient, SpeechToTextService speechToTextService)
        {
            this.botClient = botClient;
            this.speechToTextService = speechToTextService;
        }

        public bool CanHandleRequest(StudentUser studentUser, Message message)
        {
            return studentUser is not null &&
                studentUser.ConversationState == ConversationState.WaitingForProjectName;
        }

        public async Task<Response> HandleAsync(StudentUser studentUser, Message message)
        {
            var projectName = await ExtractProjectNameAsync(message);
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
                TextMessage = new()
                {
                    Text = $"Дякую, було встановлено назву '{projectName}'! " +
                            $"Будь-ласка, введіть короткий опис проекту:"
                },
                ReplyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup()
            };
        }

        private async Task<string> ExtractProjectNameAsync(Message message) =>
            message.Type switch
            {
                MessageType.Text => message.Text,
                MessageType.Voice => await botClient.RecognizeSpeechFromVoiceMessageAsync(
                    message, speechToTextService),
                _ => throw new Exception("Unsupported message type while extracting project name."),
            };
    }
}
