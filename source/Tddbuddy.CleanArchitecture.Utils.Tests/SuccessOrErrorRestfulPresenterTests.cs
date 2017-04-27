using CleanArchitecture.Utils.HttpResponses;
using CleanArchitecture.Utils.Presenters;
using CleanArchitecture.Utils.TOs;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tddbuddy.CleanArchitecture.Utils.Tests
{
    public class SuccessOrErrorRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenSuccessfullResponse_ShouldReturnOkResultWithContent()
        {
            //---------------Set up test pack-------------------
            var content = new object();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as OkObjectResult;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        [Fact]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessableEntityResultWithContent()
        {
            //---------------Set up test pack-------------------
            var content = new ErrorOutputTo();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutputTo>;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        private SuccessOrErrorRestfulPresenter<object, ErrorOutputTo> CreatePresenter()
        {
            var apiController = Substitute.For<Controller>();
            return new SuccessOrErrorRestfulPresenter<object, ErrorOutputTo>(apiController);
        }
    }
}