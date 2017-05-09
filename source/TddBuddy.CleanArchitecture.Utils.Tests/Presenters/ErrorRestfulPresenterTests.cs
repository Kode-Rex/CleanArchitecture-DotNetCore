using Microsoft.AspNetCore.Mvc;
using TddBuddy.CleanArchitecture.Utils.HttpResponses;
using TddBuddy.CleanArchitecture.Utils.Presenters;
using TddBuddy.CleanArchitecture.Utils.TOs;
using Xunit;

namespace Tddbuddy.CleanArchitecture.Utils.Tests.Presenters
{
    public class ErrorRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenNoResponse_ShouldReturnOkResult()
        {
            //---------------Set up test pack-------------------
            var presenter = CreatePresenter();
            //---------------Execute Test ----------------------
            var result = presenter.Render() as OkResult;
            //---------------Test Result -----------------------
            Assert.NotNull(result);
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

        private ErrorRestfulPresenter<ErrorOutputTo> CreatePresenter()
        {
            return new ErrorRestfulPresenter<ErrorOutputTo>();
        }
    }
}