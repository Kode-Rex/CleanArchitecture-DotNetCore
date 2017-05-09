using System;
using TddBuddy.CleanArchitecture.Utils.HttpResponses;
using TddBuddy.CleanArchitecture.Utils.Output;
using TddBuddy.CleanArchitecture.Utils.Presenters;
using TddBuddy.CleanArchitecture.Utils.TOs;
using Xunit;

namespace Tddbuddy.CleanArchitecture.Utils.Tests.Presenters
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
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutputTo>;
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

        private ErrorOutputTo CreateErrorResult()
        {
            return new ErrorOutputTo();
        }

        private IFileOutput CreateFileResult(string fileName, byte[] bytes, string contentType)
        {
            var result = new InMemoryFileOutputTo(fileName, bytes, contentType);
            return result;
        }

        private DownloadFilePresenter CreatePresenter()
        {
            return new DownloadFilePresenter();
        }

    }
}