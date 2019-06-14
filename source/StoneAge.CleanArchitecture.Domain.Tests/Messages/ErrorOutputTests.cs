using System.Collections.Generic;
using FluentAssertions;
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

        public class Ctor
        {
            [Fact]
            public void GivenString_Expect_Message_Added()
            {
                //---------------Arrange-------------------
                var error = "an error";
                //---------------Act-------------------
                var result = new ErrorOutput(error);
                //---------------Assert-------------------
                result.Errors.Should().BeEquivalentTo(new List<string> {error});
            }

            [Fact]
            public void Given_List_Of_Strings_Expect_Message_Added()
            {
                //---------------Arrange-------------------
                var errors = new List<string>
                {
                    "error 1",
                    "error 2"
                };
                //---------------Act-------------------
                var result = new ErrorOutput(errors);
                //---------------Assert-------------------
                result.Errors.Should().BeEquivalentTo(errors);
            }

            [Fact]
            public void Given_Null_List_Of_Strings_Expect_Empty_List_Instead_Of_Null()
            {
                //---------------Arrange-------------------
                var errors = (List<string>)null;
                //---------------Act-------------------
                var result = new ErrorOutput(errors);
                //---------------Assert-------------------
                result.Errors.Should().NotBeNull();
            }

            [Fact]
            public void Given_Null_String_Expect_Empty_List()
            {
                //---------------Arrange-------------------
                var error = (string)null;
                //---------------Act-------------------
                var result = new ErrorOutput(error);
                //---------------Assert-------------------
                result.Errors.Should().BeEmpty();
            }
        }

        public class AddError
        {
            [Fact]
            public void Given_Not_Null_String_Expect_It_Added_To_Errors()
            {
                //---------------Arrange-------------------
                var error = "an error";
                var sut = new ErrorOutput();
                //---------------Act-------------------
                sut.AddError(error);
                //---------------Assert-------------------
                sut.Errors.Should().BeEquivalentTo(new List<string> {error});
            }

            [Fact]
            public void Given_Null_String_Expect_It_Not_Added_To_Errors()
            {
                //---------------Arrange-------------------
                var error = (string)null;
                var sut = new ErrorOutput();
                //---------------Act-------------------
                sut.AddError(error);
                //---------------Assert-------------------
                sut.Errors.Should().BeEmpty();
            }
        }

        public class AddErrors
        {
            [Fact]
            public void Given_Not_Null_List_Expect_It_Added_To_Errors()
            {
                //---------------Arrange-------------------
                var errors = new List<string>
                {
                    "error 1",
                    "error 2"
                };
                var sut = new ErrorOutput();
                //---------------Act-------------------
                sut.AddErrors(errors);
                //---------------Assert-------------------
                sut.Errors.Should().BeEquivalentTo(errors);
            }

            [Fact]
            public void Given_Not_Null_String_Expect_It_Not_Added_To_Errors()
            {
                //---------------Arrange-------------------
                var error = "an error";
                var sut = new ErrorOutput();
                //---------------Act-------------------
                sut.AddError(error);
                //---------------Assert-------------------
                sut.Errors.Should().Contain(error);
            }
        }

    }
}
