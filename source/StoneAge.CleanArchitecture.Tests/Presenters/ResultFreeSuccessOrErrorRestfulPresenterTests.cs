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
        public void Render_GivenSuccessfulEmptyResponse_ShouldReturnOkWithoutContent()
        {
            //---------------Arrange-------------------
            var presenter = CreatePresenter();
            presenter.Respond();
            //---------------Act-------------------
            var result = presenter.Render() as OkResult;
            //---------------Assert-------------------
            Assert.NotNull(result);
        }

        [Fact]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessableEntityResultWithContent()
        {
            //---------------Arrange-------------------
            var content = new ErrorOutput();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Act-------------------
            var result = presenter.Render() as UnprocessableEntityResult<ErrorOutput>;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        private ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutput> CreatePresenter()
        {
            return new ResultFreeSuccessOrErrorRestfulPresenter<ErrorOutput>();
        }
    }
}