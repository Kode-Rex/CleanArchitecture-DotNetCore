namespace StoneAge.CleanArchitecture.Domain.Messages
{
    public class PdfFileOutput : InMemoryFileOutput
    {
        private const string _contentType = "application/pdf";

        public PdfFileOutput(string fileName, byte[] fileData) : base(fileName, fileData, _contentType)
        {
        }
    }
}