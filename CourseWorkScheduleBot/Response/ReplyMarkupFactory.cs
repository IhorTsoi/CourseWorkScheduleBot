using Telegram.Bot.Types.ReplyMarkups;

namespace CourseWorkScheduleBot
{
    public static class ReplyMarkupFactory
    {
        public static IReplyMarkup CreateDefaultKeyboardMarkup() =>
            new ReplyKeyboardMarkup(
                new KeyboardButton[] {
                    new(Commands.ViewProjectSummary),
                    new(Commands.AddDeadline)
                },
                resizeKeyboard: true
            );

        public static IReplyMarkup CreateEmptyKeyboardMarkup() => new ReplyKeyboardRemove();
    }
}
