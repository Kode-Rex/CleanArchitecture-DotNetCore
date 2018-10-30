using System.IO;

namespace StoneAge.CleanArchitecture.Domain.Output
{
    public interface IFileOutput
    {
        string FileName { get; }
        string ContentType { get; }
        Stream GetStream();
    }
}