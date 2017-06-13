using System;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.HttpResponses;
using TddBuddy.CleanArchitecture.Presenters;
using Xunit;

namespace Tddbuddy.CleanArchitecture.Tests.Presenters
{
    public class DownloadFilePresenterTests
    {
        [Fact]
        public void Render_GivenFileResponse_ShouldReturnDownloadFileResult()
        {
            //---------------Set up test pack-------------------
            var fileName = "testFile";
            var contextType = "application/json";
            var bytes = new byte[15];
            var fileOutput = CreateFileResult(fileName, bytes, contextType);

            var presenter = CreatePresenter();
            presenter.Respond(fileOutput);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as DownloadFileResult;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
            Assert.Equal(fileName, result.FileDownloadName);
            Assert.Equal(contextType, result.ContentType);
            // todo : assert stream content
        }

        [Fact]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessibleEntity()
        {
            //---------------Set up test pack-------------------
            var errorOutput = CreateErrorResult();

            var presenter = CreatePresenter();
            presenter.Respond(errorOutput);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutputMessage>;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
            Assert.Equal(errorOutput, result.Value);
        }

        [Fact]
        public void Render_GivenFileAndErrorResponse_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var errorOutput = CreateErrorResult();
            var fileOutput = CreateFileResult("fileName", new byte[1], "application/json");

            var presenter = CreatePresenter();
            presenter.Respond(errorOutput);
            presenter.Respond(fileOutput);
            //---------------Execute Test ----------------------
            var exception = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Test Result -----------------------
            Assert.Equal("Only one response allowed", exception.Message);
        }

        [Fact]
        public void Render_GivenNoResponse_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var presenter = CreatePresenter();
            //---------------Execute Test ----------------------
            var exception = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Test Result -----------------------
            Assert.Equal("No response specified", exception.Message);
        }

        private ErrorOutputMessage CreateErrorResult()
        {
            return new ErrorOutputMessage();
        }

        private IFileOutput CreateFileResult(string fileName, byte[] bytes, string contentType)
        {
            var result = new InMemoryFileOutputMessage(fileName, bytes, contentType);
            return result;
        }

        private DownloadFilePresenter CreatePresenter()
        {
            return new DownloadFilePresenter();
        }

    }
}