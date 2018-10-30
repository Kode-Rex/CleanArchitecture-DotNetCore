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
            var content = new ErrorOutput();
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Execute Test ----------------------
            var result = presenter.Render() as UnprocessasbleEntityResult<ErrorOutput>;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        private SuccessOrErrorRestfulPresenter<object, ErrorOutput> CreatePresenter()
        {
            return new SuccessOrErrorRestfulPresenter<object, ErrorOutput>();
        }
    }
}