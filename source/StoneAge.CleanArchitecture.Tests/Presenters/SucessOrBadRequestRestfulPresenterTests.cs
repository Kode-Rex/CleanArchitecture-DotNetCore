using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class SucessOrBadRequestRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenSuccessfulResponse_ShouldReturnOkResultWithContent()
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
            var result = presenter.Render() as BadRequestResult;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        private SuccessOrBadRequestRestfulPresenter<object> CreatePresenter()
        {
            return new SuccessOrBadRequestRestfulPresenter<object>();
        }
    }
}
