using System;
using System.IO;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Domain.Messages
{
    [Obsolete("Use InMemoryFileOutput instead. I found this name to be too verbose.")]
    public class InMemoryFileOutputMessage : IFileOutput
    {
        private readonly byte[] _fileData;
        public string FileName { get; }
        public string ContentType { get; private set; }

        public InMemoryFileOutputMessage(string fileName, byte[] fileData, string contentType)
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