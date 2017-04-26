using System.IO;

namespace CleanArchitecture.Utils.Output
{
    public interface IFileOutput
    {
        string FileName { get; }
        string ContentType { get; }
        Stream GetStream();
    }
}