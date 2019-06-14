using System.Collections.Generic;
using StoneAge.CleanArchitecture.Domain.Messages;
using Xunit;

namespace StoneAge.CleanArchitecture.Domain.Tests.Messages
{
    
    public class ErrorOutputTests
    {
        public class FetchErrors
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
        }
        
        public class HasErrors
        {
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

            [Fact]
            public void HasErrors_WhenErrorsNull_ShouldReturnFalse()
            {
                //---------------Arrange-------------------
                var errorList = (List<string>)null;
                var errorOutputTo = new ErrorOutput(errorList);
                //---------------Act-------------------
                var result = errorOutputTo.HasErrors;
                //---------------Assert-------------------
                Assert.False(result);
            }
        }
        
    }
}
