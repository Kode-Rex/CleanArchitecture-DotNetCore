using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace StoneAge.CleanArchitecture.HttpResponses
{
    public class DownloadFileResult : FileStreamResult
    {
        public DownloadFileResult(Stream fileStream, string contentType, string fileDownloadName) : base(fileStream, contentType)
        {
            if (string.IsNullOrWhiteSpace(fileDownloadName))
            {
                throw new ArgumentNullException(nameof(fileDownloadName));
            }

            FileDownloadName = fileDownloadName;
        }
    }
}