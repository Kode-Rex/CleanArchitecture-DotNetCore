using System.IO;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain.Messages
{
    public class InMemoryFileOutput : IFileOutput
    {
        private readonly byte[] _fileData;
        public string FileName { get; }
        public string ContentType { get; }

        public InMemoryFileOutput(string fileName, byte[] fileData, string contentType)
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