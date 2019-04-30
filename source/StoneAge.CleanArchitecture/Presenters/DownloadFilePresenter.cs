using System;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.HttpResponses;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class DownloadFilePresenter : IRespondWithSuccessOrError<IFileOutput, ErrorOutput>
    {
        private IFileOutput _fileOutput;
        private ErrorOutput _errorOutput;

        public void Respond(IFileOutput fileOutput)
        {
            _fileOutput = fileOutput;
        }

        public void Respond(ErrorOutput errorOutput)
        {
            _errorOutput = errorOutput;
        }

        public IActionResult Render()
        {
            if (IsErrorResponse() && IsFileResponse())
            {
                throw new InvalidOperationException("Only one response allowed");
            }

            if (!IsErrorResponse() && !IsFileResponse())
            {
                throw new InvalidOperationException("No response specified");
            }

            if (IsErrorResponse())
            {   
                return new UnprocessableEntityResult<ErrorOutput>(_errorOutput);
            }

            return new DownloadFileResult(_fileOutput.GetStream(), _fileOutput.ContentType, _fileOutput.FileName);
        }

        private bool IsFileResponse()
        {
            return _fileOutput != null;
        }

        private bool IsErrorResponse()
        {
            return _errorOutput != null;
        }
    }
}