using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

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