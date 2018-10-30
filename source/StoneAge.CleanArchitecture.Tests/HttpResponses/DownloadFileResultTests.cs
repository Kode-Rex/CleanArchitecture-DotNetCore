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
            //---------------Set up test pack-------------------
            var expected = "fileStream";
            //---------------Execute Test ----------------------
            var result = Assert.Throws<ArgumentNullException>(() => new DownloadFileResult(null, "application/json","fileName"));
            //---------------Test Result -----------------------
            Assert.Equal(expected, result.ParamName);
        }

        [Fact]
        public void Ctor_WhenNullContentType_ShouldThrowException()
        {
            //---------------Set up test pack-------------------
            var expected = "The header contains invalid values at index 0: '<null>'";
            //---------------Execute Test ----------------------
            using (var stream = new MemoryStream())
            {
                var result = Assert.Throws<FormatException>(() => new DownloadFileResult(stream, null, "filename"));
                //---------------Test Result -----------------------
                Assert.Equal(expected, result.Message);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void Ctor_WhenNullOrWhiteSpaceFileDownloadName_ShouldThrowException(string fileName)
        {
            //---------------Set up test pack-------------------
            var expected = "fileDownloadName";
            //---------------Execute Test ----------------------
            using (var stream = new MemoryStream())
            {
                var result = Assert.Throws<ArgumentNullException>(() => new DownloadFileResult(stream, "application/json", fileName));
                //---------------Test Result -----------------------
                Assert.Equal(expected, result.ParamName);
            }
        }

        [Fact]
        public void Ctor_WhenValidInput_ShouldNotThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Execute Test ----------------------
            using (var stream = new MemoryStream())
            {
                var result = Record.Exception( () => new DownloadFileResult(stream, "application/json", "filename"));
                //---------------Test Result -----------------------
                Assert.Null(result);
            }
        }
    }
}
