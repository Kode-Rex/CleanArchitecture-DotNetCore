namespace TddBuddy.CleanArchitecture.Domain.Messages
{
#pragma warning disable 618
    public class InMemoryFileOutput : InMemoryFileOutputMessage 
#pragma warning restore 618
    {
        public InMemoryFileOutput(string fileName, byte[] fileData, string contentType) : base(fileName, fileData, contentType)
        {
        }
    }
}