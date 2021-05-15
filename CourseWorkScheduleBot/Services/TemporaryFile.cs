using System;
using System.IO;

namespace CourseWorkScheduleBot.Services
{
    class TemporaryFile : IDisposable
    {
        private readonly string filePath;

        public TemporaryFile()
        {
            filePath = Path.GetTempFileName();
        }

        public Stream GetWriteStream() => File.OpenWrite(filePath);

        public Stream GetReadStream() => File.OpenRead(filePath);

        public void Dispose() => File.Delete(filePath);
    }
}
