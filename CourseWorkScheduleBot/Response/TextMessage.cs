using Telegram.Bot.Types.Enums;

namespace CourseWorkScheduleBot.Messages
{
    public class TextMessage
    {
        public string Text { get; set; }
        public ParseMode ParseMode { get; set; } = ParseMode.Default;
    }
}
