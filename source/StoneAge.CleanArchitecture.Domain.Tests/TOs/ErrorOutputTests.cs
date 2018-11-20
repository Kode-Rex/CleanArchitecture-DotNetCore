using StoneAge.CleanArchitecture.Domain.Messages;
using Xunit;

namespace StoneAge.CleanArchitecture.Domain.Tests.TOs
{
    
    public class ErrorOutputTests
    {
        [Fact]
        public void FetchErrors_WhenConstructed_ShouldReturnList()
        {
            //---------------Arrange-------------------
            //---------------Act-------------------
            var result = new ErrorOutput();
            //---------------Assert-------------------
            Assert.NotNull(result.Errors);
        }

        [Fact]
        public void HasErrors_WhenNoErrors_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var errorOutputTo = new ErrorOutput();
            //---------------Act-------------------
            var result = errorOutputTo.HasErrors;
            //---------------Assert-------------------
            Assert.False(result);
        }

        [Fact]
        public void HasErrors_WhenErrors_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var errorOutputTo = new ErrorOutput();
            errorOutputTo.AddError("test error");
            //---------------Act-------------------
            var result = errorOutputTo.HasErrors;
            //---------------Assert-------------------
            Assert.True(result);
        }
    }
}
