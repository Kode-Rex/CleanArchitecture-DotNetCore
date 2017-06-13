using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenters;
using Xunit;

namespace TddBuddy.CleanArchitecture.Domain.Tests.Presenter
{
    public class PropertyPresenterTests
    {
        [Fact]
        public void IsErrorResponse_WhenNoErrors_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var presenter = new PropertyPresenter<object, ErrorOutputMessage>();
            //---------------Execute Test ----------------------
            var result = presenter.IsErrorResponse();
            //---------------Test Result -----------------------
            Assert.False(result);
        }

        [Fact]
        public void IsErrorResponse_WhenErrors_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var presenter = new PropertyPresenter<object, ErrorOutputMessage>();
            presenter.Respond(new ErrorOutputMessage());
            //---------------Execute Test ----------------------
            var result = presenter.IsErrorResponse();
            //---------------Test Result -----------------------
            Assert.True(result);
        }

        [Fact]
        public void ErrorContent_WhenErrors_ShouldReturnErrorTo()
        {
            //---------------Set up test pack-------------------
            var errors = new ErrorOutputMessage();
            var presenter = new PropertyPresenter<object, ErrorOutputMessage>();
            presenter.Respond(errors);
            //---------------Execute Test ----------------------
            var result = presenter.ErrorContent;
            //---------------Test Result -----------------------
            Assert.Equal(errors, result);
        }

        [Fact]
        public void ErrorContent_WhenNoErrors_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------
            var presenter = new PropertyPresenter<object, ErrorOutputMessage>();
            //---------------Execute Test ----------------------
            var result = presenter.ErrorContent;
            //---------------Test Result -----------------------
            Assert.Null(result);
        }

        [Fact]
        public void SuccessContent_WhenSet_ShouldReturnObject()
        {
            //---------------Set up test pack-------------------
            var respondWith = new object();
            var presenter = new PropertyPresenter<object, ErrorOutputMessage>();
            presenter.Respond(respondWith);
            //---------------Execute Test ----------------------
            var result = presenter.SuccessContent;
            //---------------Test Result -----------------------
            Assert.Equal(respondWith, result);
        }

        [Fact]
        public void SuccessContent_WhenNotSet_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------
            var presenter = new PropertyPresenter<object, ErrorOutputMessage>();
            //---------------Execute Test ----------------------
            var result = presenter.SuccessContent;
            //---------------Test Result -----------------------
            Assert.Null(result);
        }
    }
}
