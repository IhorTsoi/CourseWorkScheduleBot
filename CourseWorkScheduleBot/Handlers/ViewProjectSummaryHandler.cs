using CourseWorkScheduleBot.Messages;
using CourseWorkScheduleBot.Models;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CourseWorkScheduleBot.Handlers
{
    class ViewProjectSummaryHandler : IHandler
    {
        public bool CanHandleRequest(StudentUser studentUser, Message message)
        {
            return
                studentUser is not null &&
                studentUser.ConversationState == ConversationState.ProjectRegistered &&
                message.Type == MessageType.Text &&
                message.Text.Equals(Commands.ViewProjectSummary);
        }

        public Response Handle(StudentUser studentUser, Message message)
        {
            var projectSummaryMessage = ConstructProjectSummaryMessage(studentUser.Project);

            return new()
            {
                TextMessage = projectSummaryMessage,
                ReplyMarkup = ReplyMarkupFactory.CreateDefaultKeyboardMarkup()
            };
        }

        private TextMessage ConstructProjectSummaryMessage(Project project)
        {
            var deadlinesText = project.Deadlines.Count > 0 ?
                StringifyDeadlines(project.Deadlines) :
                "[ немає ]";
            var summaryText =
                $"<b>Назва проекту</b>: {project.Name}\n" +
                $"<b>Короткий опис проекту</b>: {project.Description}\n" +
                $"\n" +
                $"<b>Дедлайни</b>: {deadlinesText}";

            return new() { Text = summaryText, ParseMode = ParseMode.Html };
        }

        private string StringifyDeadlines(List<string> deadlines)
        {
            const string prefix = "\n\t\t⏰ ";
            return prefix + string.Join(prefix, deadlines);
        }
    }
}
