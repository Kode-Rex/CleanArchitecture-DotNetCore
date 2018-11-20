using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class SucessOrUnauthorizedRestfulPresenterTests
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
            var presenter = CreatePresenter();
            presenter.Respond();
            //---------------Act-------------------
            var result = presenter.Render() as UnauthorizedResult;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        private SuccessOrUnauthorizedRestfulPresenter<object> CreatePresenter()
        {
            return new SuccessOrUnauthorizedRestfulPresenter<object>();
        }
    }
}
