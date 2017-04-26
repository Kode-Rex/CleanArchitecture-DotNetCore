using CleanArchitecture.Utils.HttpResponses;
using CleanArchitecture.Utils.Presenters;
using CleanArchitecture.Utils.TOs;
using NSubstitute;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Utils.Tests.Presenters
{
    [TestFixture]
    public class ErrorRestfulPresenterTests
    {
        [Test]
        public void Render_GivenNoResponse_ShouldReturnOkResult()
        {
            //---------------Set up test pack-------------------
            var presenter = CreatePresenter();
            //---------------Execute Test ----------------------
            var result = presenter.Render() as OkResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }

        [Test]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessableEntityResultWithContent()
        {
            //---------------Set up test pack-------------------
            var content = new ErrorOutputTo();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutputTo>;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            Assert.AreEqual(content, result.Value);
        }

        private ErrorRestfulPresenter<ErrorOutputTo> CreatePresenter()
        {
            var apiController = Substitute.For<Controller>();
            return new ErrorRestfulPresenter<ErrorOutputTo>(apiController);
        }
    }
}