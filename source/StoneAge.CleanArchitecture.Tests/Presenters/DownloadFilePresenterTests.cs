using System;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.HttpResponses;
using StoneAge.CleanArchitecture.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class DownloadFilePresenterTests
    {
        [Fact]
        public void Render_GivenFileResponse_ShouldReturnDownloadFileResult()
        {
            //---------------Arrange-------------------
            var fileName = "testFile";
            var contextType = "application/json";
            var streamSize = 15;
            var bytes = CreateBytes(streamSize);

            var resultStream = new byte[streamSize];
            var fileOutput = CreateFileResult(fileName, bytes, contextType);

            var presenter = CreatePresenter();
            presenter.Respond(fileOutput);
            //---------------Act-------------------
            var result = presenter.Render() as DownloadFileResult;
            //---------------Assert-------------------
            Assert.Equal(fileName, result.FileDownloadName);
            Assert.Equal(contextType, result.ContentType);
            AssertFileStreamCorrect(streamSize, result, resultStream, bytes);
        }

        [Fact]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessibleEntity()
        {
            //---------------Arrange-------------------
            var errorOutput = CreateErrorResult();

            var presenter = CreatePresenter();
            presenter.Respond(errorOutput);
            //---------------Act-------------------
            var result = presenter.Render() as UnprocessableEntityResult<ErrorOutput>;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(errorOutput, result.Value);
        }

        [Fact]
        public void Render_GivenFileAndErrorResponse_ShouldThrowException()
        {
            //---------------Arrange-------------------
            var errorOutput = CreateErrorResult();
            var fileOutput = CreateFileResult("fileName", new byte[1], "application/json");

            var presenter = CreatePresenter();
            presenter.Respond(errorOutput);
            presenter.Respond(fileOutput);
            //---------------Act-------------------
            var exception = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Assert-------------------
            Assert.Equal("Only one response allowed", exception.Message);
        }

        [Fact]
        public void Render_GivenNoResponse_ShouldThrowException()
        {
            //---------------Arrange-------------------
            var presenter = CreatePresenter();
            //---------------Act-------------------
            var exception = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Assert-------------------
            Assert.Equal("No response specified", exception.Message);
        }

        private static void AssertFileStreamCorrect(int streamSize, FileStreamResult result, byte[] resultStream, byte[] bytes)
        {
            Assert.Equal(streamSize, result.FileStream.Length);
            result.FileStream.Read(resultStream, 0, streamSize);
            Assert.Equal(bytes, resultStream);
        }

        private static byte[] CreateBytes(int streamSize)
        {
            var bytes = new byte[streamSize];
            bytes[3] = 42;
            bytes[9] = 42;
            return bytes;
        }

        private ErrorOutput CreateErrorResult()
        {
            return new ErrorOutput();
        }

        private IFileOutput CreateFileResult(string fileName, byte[] bytes, string contentType)
        {
            var result = new InMemoryFileOutput(fileName, bytes, contentType);
            return result;
        }

        private DownloadFilePresenter CreatePresenter()
        {
            return new DownloadFilePresenter();
        }

    }
}