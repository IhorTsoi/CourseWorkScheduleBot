using CourseWorkScheduleBot.Services;
using CourseWorkScheduleBot.Storage;
using System.Collections.Generic;
using Telegram.Bot;

namespace CourseWorkScheduleBot.Handlers.Utils
{
    public static class HandlerUtils
    {
        public static IEnumerable<IHandler> CreateAllHandlers(
            TelegramBotClient bot, LocalStorage storage, SpeechToTextService speechToTextService)
        {
            return new IHandler[]
            {
                new StartHandler(storage),
                new SetProjectNameHandler(bot, speechToTextService),
                new SetProjectDescriptionHandler(bot, speechToTextService),
                new ViewProjectSummaryHandler(),
                new AddDeadlineHandler(),
                new UnknownCommandHandler()
            };
        }
    }
}
