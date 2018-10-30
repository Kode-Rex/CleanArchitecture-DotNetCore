using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.HttpResponses;
using StoneAge.CleanArchitecture.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
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
            var content = new ErrorOutput();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutput>;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        private ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutput> CreatePresenter()
        {
            return new ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutput>();
        }
    }
}