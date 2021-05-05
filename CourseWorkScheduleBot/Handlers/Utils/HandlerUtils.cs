using CourseWorkScheduleBot.Storage;
using System.Collections.Generic;

namespace CourseWorkScheduleBot.Handlers.Utils
{
    public static class HandlerUtils
    {
        public static IEnumerable<IHandler> CreateAllHandlers(LocalStorage storage)
        {
            return new IHandler[]
            {
                new StartHandler(storage),
                new SetProjectNameHandler(),
                new SetProjectDescriptionHandler(),
                new ViewProjectSummaryHandler(),
                new AddDeadlineHandler(),
                new UnknownCommandHandler()
            };
        }
    }
}
