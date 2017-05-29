using System.IO;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Domain.TOs
{
    public class InMemoryFileOutputTo : IFileOutput
    {
        private readonly byte[] _fileData;
        public string FileName { get; }
        public string ContentType { get; private set; }

        public InMemoryFileOutputTo(string fileName, byte[] fileData, string contentType)
        {
            _fileData = fileData;
            FileName = fileName;
            ContentType = contentType;
        }

        public Stream GetStream()
        {
            return new MemoryStream(_fileData);
        }
    }
}