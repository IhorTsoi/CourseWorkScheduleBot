using CourseWorkScheduleBot.Handlers.Utils;
using CourseWorkScheduleBot.Services;
using CourseWorkScheduleBot.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace CourseWorkScheduleBot
{
    class Program
    {
        static IConfiguration configuration;
        static TelegramBotClient botClient;
        static LocalStorage storage;
        static SpeechToTextService speechToTextService;

        static async Task Main()
        {
            InitializeConfiguration();
            botClient = new(configuration["BOT_API_KEY"]);
            storage = new();
            speechToTextService = new(configuration["GCP_SPEECH_TO_TEXT_CREDENTIALS"]);

            await InitializeConsoleScreenAsync();

            botClient.OnMessage += BotClient_OnMessageAsync;
            botClient.StartReceiving(new UpdateType[] { UpdateType.Message });

            Console.WriteLine("The bot is running.");
            Console.WriteLine("Press <ENTER> to stop the bot.");
            Console.ReadLine();

            botClient.StopReceiving();
        }

        private static void InitializeConfiguration()
        {
            const string jsonConfigurationFileName = "appsettings.json";
            configuration = new ConfigurationBuilder()
                .AddJsonFile(jsonConfigurationFileName, optional: false, reloadOnChange: true)
#if DEBUG
                .AddUserSecrets(typeof(Program).Assembly)
#endif
                .Build();
        }

        private static async Task InitializeConsoleScreenAsync()
        {
            var botInformation = await botClient.GetMeAsync();
            Console.Title = botInformation.Username;

            Console.OutputEncoding = Encoding.Unicode;
        }

        static async void BotClient_OnMessageAsync(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            var userId = e.Message.From.Id;
            var chatId = e.Message.Chat.Id;

            var studentUser = storage.IncludesUserWithId(userId) ?
                storage.GetUserById(userId) :
                null;

            var handler = HandlerUtils.CreateAllHandlers(botClient, storage, speechToTextService)
                .FirstOrDefault(h => h.CanHandleRequest(studentUser, message));
            if (handler is null)
            {
                throw new Exception("None of handlers matched.");
            }

            var response = await handler.HandleAsync(studentUser, message);
            await SendResponseAsync(chatId, response);
        }

        private static Task SendResponseAsync(long chatId, Response response) =>
            botClient.SendTextMessageAsync(
                chatId,
                response.TextMessage.Text,
                parseMode: response.TextMessage.ParseMode,
                replyMarkup: response.ReplyMarkup);
    }
}
