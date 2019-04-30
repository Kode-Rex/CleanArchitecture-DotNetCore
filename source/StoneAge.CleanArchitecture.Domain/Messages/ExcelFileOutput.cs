namespace StoneAge.CleanArchitecture.Domain.Messages
{
    public class ExcelFileOutput : InMemoryFileOutput
    {
        private const string _contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelFileOutput(string fileName, byte[] fileData) : base(fileName, fileData, _contentType)
        {
        }
    }
}