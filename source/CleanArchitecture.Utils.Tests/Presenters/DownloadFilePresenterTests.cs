using System;
using CleanArchitecture.Utils.HttpResponses;
using CleanArchitecture.Utils.Output;
using CleanArchitecture.Utils.Presenters;
using CleanArchitecture.Utils.TOs;
using NUnit.Framework;

namespace CleanArchitecture.Utils.Tests.Presenters
{
    [TestFixture]
    public class DownloadFilePresenterTests
    {
        [Test]
        public void Render_GivenFileResponse_ShouldReturnDownloadFileResult()
        {
            //---------------Set up test pack-------------------
            var fileOutput = CreateFileResult();

            var presenter = CreatePresenter();
            presenter.Respond(fileOutput);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as DownloadFileResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            Assert.AreEqual(fileOutput.FileName, result.FileDownloadName);
            Assert.AreEqual(fileOutput.GetStream().Length, result.FileStream.Length);
        }

        [Test]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessibleEntity()
        {
            //---------------Set up test pack-------------------
            var errorOutput = CreateErrorResult();

            var presenter = CreatePresenter();
            presenter.Respond(errorOutput);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutputTo>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            Assert.AreEqual(errorOutput, result.Value);
        }

        [Test]
        public void Render_GivenFileAndErrorResponse_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var errorOutput = CreateErrorResult();
            var fileOutput = CreateFileResult();

            var presenter = CreatePresenter();
            presenter.Respond(errorOutput);
            presenter.Respond(fileOutput);
            //---------------Execute Test ----------------------
            var exception = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Test Result -----------------------
            Assert.AreEqual("Only one response allowed", exception.Message);
        }

        [Test]
        public void Render_GivenNoResponse_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var presenter = CreatePresenter();
            //---------------Execute Test ----------------------
            var exception = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Test Result -----------------------
            Assert.AreEqual("No response specified", exception.Message);
        }

        private ErrorOutputTo CreateErrorResult()
        {
            return new ErrorOutputTo();
        }

        private IFileOutput CreateFileResult()
        {
            return Substitute.For<IFileOutput>();
        }

        private DownloadFilePresenter CreatePresenter()
        {
            return new DownloadFilePresenter();
        }

    }
}