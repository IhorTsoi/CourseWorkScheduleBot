using CourseWorkScheduleBot.Models;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers
{
    public interface IHandler
    {
        bool CanHandleRequest(StudentUser studentUser, Message message);
        Task<Response> HandleAsync(StudentUser studentUser, Message message);
    }
}
