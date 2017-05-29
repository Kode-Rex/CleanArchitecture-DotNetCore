using System.IO;

namespace TddBuddy.CleanArchitecture.Domain.Output
{
    public interface IFileOutput
    {
        string FileName { get; }
        string ContentType { get; }
        Stream GetStream();
    }
}