using System;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.HttpResponses;
using StoneAge.CleanArchitecture.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class ErrorRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenNoResponse_ShouldThrowInvalidOperationException()
        {
            //---------------Arrange-------------------
            var presenter = CreatePresenter();
            //---------------Act-------------------
            var result = Assert.Throws<InvalidOperationException>(() => presenter.Render());
            //---------------Assert-------------------
            Assert.Equal("No response specified.", result.Message);
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

        private ErrorRestfulPresenter<ErrorOutput> CreatePresenter()
        {
            return new ErrorRestfulPresenter<ErrorOutput>();
        }
    }
}