using Google.Cloud.Speech.V1;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Google.Cloud.Speech.V1.RecognitionConfig.Types;

namespace CourseWorkScheduleBot.Services
{
    public class SpeechToTextService
    {
        private readonly SpeechClient speechClient;

        public SpeechToTextService(string jsonApiCredentials)
        {
            speechClient = new SpeechClientBuilder()
            {
                JsonCredentials = jsonApiCredentials
            }.Build();
        }

        public Task<string> RecognizeFromStreamAsync(Stream streamWithVoice)
        {
            var audio = RecognitionAudio.FromStream(streamWithVoice);
            var configuration = new RecognitionConfig
            {
                Encoding = AudioEncoding.OggOpus,
                SampleRateHertz = 48000,
                LanguageCode = LanguageCodes.Ukrainian.Ukraine,
            };
            var response = speechClient.Recognize(configuration, audio);

            return Task.FromResult(StringifyRecognizedSpeech(response));
        }

        private static string StringifyRecognizedSpeech(RecognizeResponse response)
        {
            if (response.Results.Count == 0)
            {
                return string.Empty;
            }

            var recognizedTextParts = response.Results.Select(
                r => r.Alternatives[0].Transcript);
            var recognizedText = string.Join("", recognizedTextParts);
            return recognizedText;
        }
    }
}
