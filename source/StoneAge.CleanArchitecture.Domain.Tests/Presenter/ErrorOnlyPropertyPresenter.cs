using FluentAssertions;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Domain.Tests.Presenter
{
    public class ErrorOnlyPropertyPresenterTests
    {
        [Fact]
        public void Given_No_Errors_Expect_Null_Error_Object()
        {
            //---------------Arrange-------------------
            var presenter = new ErrorOnlyPropertyPresenter<ErrorOutput>();
            //---------------Act-------------------
            presenter.Respond();
            //---------------Assert-------------------
            presenter.Output.Should().BeNull();
        }

        [Fact]
        public void Given_Errors_Expect_Non_Null_Error_Object()
        {
            //---------------Arrange-------------------
            var errors = new ErrorOutput();
            errors.AddError("Test error 1");
            errors.AddError("Test error 2");
            var presenter = new ErrorOnlyPropertyPresenter<ErrorOutput>();
            //---------------Act-------------------
            presenter.Respond(errors);
            //---------------Assert-------------------
            presenter.Output.Should().NotBeNull();
        }
    }
}
