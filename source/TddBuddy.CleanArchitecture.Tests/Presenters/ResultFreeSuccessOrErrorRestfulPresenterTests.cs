using Microsoft.AspNetCore.Mvc;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.HttpResponses;
using TddBuddy.CleanArchitecture.Presenters;
using Xunit;

namespace Tddbuddy.CleanArchitecture.Tests.Presenters
{
    public class ResultFreeSuccessOrErrorRestfulPresenterTests
    {
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

        private ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage> CreatePresenter()
        {
            return new ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutputMessage>();
        }
    }
}