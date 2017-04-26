using System;
using CleanArchitecture.Utils.HttpResponses;
using CleanArchitecture.Utils.Output;
using CleanArchitecture.Utils.TOs;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Utils.Presenters
{
    public class DownloadFilePresenter : IRespondWithSuccessOrError<IFileOutput, ErrorOutputTo>, IRespondWith<IFileOutput>
    {
        private IFileOutput _fileOutput;
        private ErrorOutputTo _errorOutput;

        public void Respond(IFileOutput fileOutput)
        {
            _fileOutput = fileOutput;
        }

        public void Respond(ErrorOutputTo errorOutput)
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
                return new UnprocessasbleEntityResult<ErrorOutputTo>(_errorOutput);
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