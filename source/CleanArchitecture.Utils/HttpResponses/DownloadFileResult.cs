using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Utils.HttpResponses
{
    public class DownloadFileResult : FileStreamResult
    {
        public DownloadFileResult(Stream fileStream, string contentType, string fileDownloadName) : base(fileStream, contentType)
        {
            FileDownloadName = fileDownloadName;
        }
    }
}