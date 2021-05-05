using CourseWorkScheduleBot.Messages;
using Telegram.Bot.Types.ReplyMarkups;

namespace CourseWorkScheduleBot
{
    public class Response
    {
        public TextMessage TextMessage { get; set; }
        public IReplyMarkup ReplyMarkup { get; set; }
    }
}
