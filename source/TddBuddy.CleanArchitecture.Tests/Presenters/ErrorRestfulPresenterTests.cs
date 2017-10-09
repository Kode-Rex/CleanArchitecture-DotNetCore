using System;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.HttpResponses;
using TddBuddy.CleanArchitecture.Presenters;
using Xunit;

namespace Tddbuddy.CleanArchitecture.Tests.Presenters
{
    public class ErrorRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenNoResponse_ShouldThrowInvalidOperationException()
        {
            //---------------Set up test pack-------------------
            var presenter = CreatePresenter();
            //---------------Execute Test ----------------------
            var result = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Test Result -----------------------
            Assert.Equal("No response specified.", result.Message);
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

        private ErrorRestfulPresenter<ErrorOutputMessage> CreatePresenter()
        {
            return new ErrorRestfulPresenter<ErrorOutputMessage>();
        }
    }
}