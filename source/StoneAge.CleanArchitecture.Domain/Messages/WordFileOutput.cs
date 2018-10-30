namespace StoneAge.CleanArchitecture.Domain.Messages
{
    public class WordFileOutput : InMemoryFileOutput
    {
        private const string _contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        public WordFileOutput(string fileName, byte[] fileData) : base(fileName, fileData, _contentType)
        {
        }
    }
}