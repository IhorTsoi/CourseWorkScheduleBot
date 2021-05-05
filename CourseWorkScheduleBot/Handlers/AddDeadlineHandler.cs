﻿using CourseWorkScheduleBot.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CourseWorkScheduleBot.Handlers
{
    class AddDeadlineHandler : IHandler
    {
        public bool CanHandleRequest(StudentUser studentUser, Message message)
        {
            return
                studentUser is not null &&
                (
                    RequestIsProvidedDeadline(studentUser) ||
                    RequestIsAddDeadlineCommand(studentUser, message)
                );
        }

        public Response Handle(StudentUser studentUser, Message message)
        {
            if (RequestIsProvidedDeadline(studentUser))
            {
                return HandleProvidedDeadline(studentUser, message);
            }
            else
            {
                return HandleAddDeadlineCommand(studentUser);
            }
        }

        private Response HandleProvidedDeadline(StudentUser studentUser, Message message)
        {
            var deadline = message.Text;
            if (string.IsNullOrEmpty(deadline))
            {
                return new()
                {
                    TextMessage = new() { Text = "Помилка в інформації про дедлайн! Введіть ще раз." },
                    ReplyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup()
                };
            }

            studentUser.Project.Deadlines.Add(deadline);
            studentUser.ConversationState = ConversationState.ProjectRegistered;

            return new()
            {
                TextMessage = new() { Text = "Дані збережено!" },
                ReplyMarkup = ReplyMarkupFactory.CreateDefaultKeyboardMarkup()
            };
        }

        private Response HandleAddDeadlineCommand(StudentUser studentUser)
        {
            studentUser.ConversationState = ConversationState.WaitingForDeadline;
            return new()
            {
                TextMessage = new() { Text = "Введіть інформацію про дедлайн:" },
                ReplyMarkup = ReplyMarkupFactory.CreateEmptyKeyboardMarkup()
            };
        }

        private bool RequestIsProvidedDeadline(StudentUser studentUser)
        {
            return studentUser.ConversationState == ConversationState.WaitingForDeadline;
        }

        private bool RequestIsAddDeadlineCommand(StudentUser studentUser, Message message)
        {
            return
                studentUser.ConversationState == ConversationState.ProjectRegistered &&
                message.Type == MessageType.Text &&
                message.Text.Equals(Commands.AddDeadline);
        }
    }
}
