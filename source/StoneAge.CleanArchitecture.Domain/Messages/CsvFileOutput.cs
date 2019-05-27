namespace StoneAge.CleanArchitecture.Domain.Messages
{
    public class CsvFileOutput : InMemoryFileOutput
    {
        private const string _contentType = "text/csv";

        public CsvFileOutput(string fileName, byte[] fileData) : base(fileName, fileData, _contentType)
        {
        }
    }
}