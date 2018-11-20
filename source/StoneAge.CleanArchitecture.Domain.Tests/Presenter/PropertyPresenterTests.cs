using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Domain.Tests.Presenter
{
    public class PropertyPresenterTests
    {
        [Fact]
        public void IsErrorResponse_WhenNoErrors_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var presenter = new PropertyPresenter<object, ErrorOutput>();
            //---------------Act-------------------
            var result = presenter.IsErrorResponse();
            //---------------Assert-------------------
            Assert.False(result);
        }

        [Fact]
        public void IsErrorResponse_WhenErrors_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var presenter = new PropertyPresenter<object, ErrorOutput>();
            presenter.Respond(new ErrorOutput());
            //---------------Act-------------------
            var result = presenter.IsErrorResponse();
            //---------------Assert-------------------
            Assert.True(result);
        }

        [Fact]
        public void ErrorContent_WhenErrors_ShouldReturnErrorTo()
        {
            //---------------Arrange-------------------
            var errors = new ErrorOutput();
            var presenter = new PropertyPresenter<object, ErrorOutput>();
            presenter.Respond(errors);
            //---------------Act-------------------
            var result = presenter.ErrorContent;
            //---------------Assert-------------------
            Assert.Equal(errors, result);
        }

        [Fact]
        public void ErrorContent_WhenNoErrors_ShouldReturnNull()
        {
            //---------------Arrange-------------------
            var presenter = new PropertyPresenter<object, ErrorOutput>();
            //---------------Act-------------------
            var result = presenter.ErrorContent;
            //---------------Assert-------------------
            Assert.Null(result);
        }

        [Fact]
        public void SuccessContent_WhenSet_ShouldReturnObject()
        {
            //---------------Arrange-------------------
            var respondWith = new object();
            var presenter = new PropertyPresenter<object, ErrorOutput>();
            presenter.Respond(respondWith);
            //---------------Act-------------------
            var result = presenter.SuccessContent;
            //---------------Assert-------------------
            Assert.Equal(respondWith, result);
        }

        [Fact]
        public void SuccessContent_WhenNotSet_ShouldReturnNull()
        {
            //---------------Arrange-------------------
            var presenter = new PropertyPresenter<object, ErrorOutput>();
            //---------------Act-------------------
            var result = presenter.SuccessContent;
            //---------------Assert-------------------
            Assert.Null(result);
        }
    }
}
