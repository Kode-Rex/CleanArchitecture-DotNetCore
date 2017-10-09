using Microsoft.AspNetCore.Mvc;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.HttpResponses;
using TddBuddy.CleanArchitecture.Presenters;
using Xunit;

namespace Tddbuddy.CleanArchitecture.Tests.Presenters
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
        public void Render_GivenSuccessfullEmptyResponse_ShouldReturnOkWithoutContent()
        {
            //---------------Set up test pack-------------------
            var presenter = CreatePresenter();
            presenter.Respond();
            //---------------Execute Test ----------------------
            var result = presenter.Render() as OkResult;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
        }

        [Fact]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessableEntityResultWithContent()
        {
            //---------------Set up test pack-------------------
            var content = new ErrorOutputMessage();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutputMessage>;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        private SuccessOrErrorRestfulPresenter<object, ErrorOutputMessage> CreatePresenter()
        {
            return new SuccessOrErrorRestfulPresenter<object, ErrorOutputMessage>();
        }
    }
}