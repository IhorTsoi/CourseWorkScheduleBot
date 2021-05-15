using CourseWorkScheduleBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CourseWorkScheduleBot.Handlers.Extensions
{
    public static class VoiceMessageSpeechRecognitionExtension
    {
        public static async Task<string> RecognizeSpeechFromVoiceMessageAsync(
            this TelegramBotClient bot, Message message, SpeechToTextService speechToTextService)
        {
            using var tempFile = new TemporaryFile();
            await bot.DownloadVoiceMessageFileAsync(message.Voice.FileId, tempFile);
            return await RecognizeSpeechFromFileAsync(tempFile, speechToTextService);
        }

        private static async Task DownloadVoiceMessageFileAsync(
            this TelegramBotClient bot, string fileId, TemporaryFile tempFile)
        {
            using var tempFileWriteStream = tempFile.GetWriteStream();
            var voiceFileInformation = await bot.GetFileAsync(fileId);
            await bot.DownloadFileAsync(voiceFileInformation.FilePath, tempFileWriteStream);
        }

        private static Task<string> RecognizeSpeechFromFileAsync(
            TemporaryFile tempFile, SpeechToTextService speechToTextService)
        {
            using var readStream = tempFile.GetReadStream();
            return speechToTextService.RecognizeFromStreamAsync(readStream);
        }
    }
}
