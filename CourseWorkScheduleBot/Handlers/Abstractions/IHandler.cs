using CourseWorkScheduleBot.Models;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers
{
    public interface IHandler
    {
        bool CanHandleRequest(StudentUser studentUser, Message message);
        Response Handle(StudentUser studentUser, Message message);
    }
}
