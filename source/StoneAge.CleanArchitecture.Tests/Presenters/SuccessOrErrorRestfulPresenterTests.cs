using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.HttpResponses;
using StoneAge.CleanArchitecture.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class SuccessOrErrorRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenSuccessfullResponse_ShouldReturnOkResultWithContent()
        {
            //---------------Arrange-------------------
            var content = new object();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Act-------------------
            var result = presenter.Render() as OkObjectResult;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        [Fact]
        public void Render_GivenErrorResponse_ShouldReturnUnprocessableEntityResultWithContent()
        {
            //---------------Arrange-------------------
            var content = new ErrorOutput();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Act-------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutput>;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        private SuccessOrErrorRestfulPresenter<object, ErrorOutput> CreatePresenter()
        {
            return new SuccessOrErrorRestfulPresenter<object, ErrorOutput>();
        }
    }
}