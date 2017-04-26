using CleanArchitecture.Utils.HttpResponses;
using CleanArchitecture.Utils.Presenters;
using CleanArchitecture.Utils.TOs;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Utils.Tests.Presenters
{
    [TestFixture]
    public class SuccessOrErrorRestfulPresenterTests
    {
        [Test]
        public void Render_GivenSuccessfullResponse_ShouldReturnOkResultWithContent()
        {
            //---------------Set up test pack-------------------
            var content = new object();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as OkObjectResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            Assert.AreEqual(content, result.Value);
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

        private SuccessOrErrorRestfulPresenter<object, ErrorOutputTo> CreatePresenter()
        {
            var apiController = Substitute.For<Controller>();
            return new SuccessOrErrorRestfulPresenter<object, ErrorOutputTo>(apiController);
        }
    }
}