using System;
using System.IO;
using StoneAge.CleanArchitecture.HttpResponses;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.HttpResponses
{
    public class DownloadFileResultTests
    {
        [Fact]
        public void Ctor_WhenNullStream_ShouldThrowException()
        {
            //---------------Arrange-------------------
            var expected = "fileStream";
            //---------------Act-------------------
            var result = Assert.Throws<ArgumentNullException>(() => new DownloadFileResult(null, "application/json","fileName"));
            //---------------Assert-------------------
            Assert.Equal(expected, result.ParamName);
        }

        [Fact]
        public void Ctor_WhenNullContentType_ShouldThrowException()
        {
            //---------------Arrange-------------------
            var expected = "The header contains invalid values at index 0: '<null>'";
            //---------------Act-------------------
            using (var stream = new MemoryStream())
            {
                var result = Assert.Throws<FormatException>(() => new DownloadFileResult(stream, null, "filename"));
                //---------------Assert-------------------
                Assert.Equal(expected, result.Message);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void Ctor_WhenNullOrWhiteSpaceFileDownloadName_ShouldThrowException(string fileName)
        {
            //---------------Arrange-------------------
            var expected = "fileDownloadName";
            //---------------Act-------------------
            using (var stream = new MemoryStream())
            {
                var result = Assert.Throws<ArgumentNullException>(() => new DownloadFileResult(stream, "application/json", fileName));
                //---------------Assert-------------------
                Assert.Equal(expected, result.ParamName);
            }
        }

        [Fact]
        public void Ctor_WhenValidInput_ShouldNotThrowException()
        {
            //---------------Arrange-------------------
            //---------------Act-------------------
            using (var stream = new MemoryStream())
            {
                var result = Record.Exception( () => new DownloadFileResult(stream, "application/json", "filename"));
                //---------------Assert-------------------
                Assert.Null(result);
            }
        }
    }
}
