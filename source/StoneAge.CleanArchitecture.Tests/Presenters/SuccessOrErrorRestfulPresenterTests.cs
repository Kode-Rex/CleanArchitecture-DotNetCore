using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.HttpResponses;
using StoneAge.CleanArchitecture.Presenters;
using StoneAge.CleanArchitecture.Tests.Presenters.TestObjects;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class SuccessOrErrorRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenSuccessfulResponse_ShouldReturnOkResultWithContent()
        {
            //---------------Arrange-------------------
            var content = new DomainObject
            {
                Name = "Travis",
                Age = 38
            };
            var presenter = CreatePresenter();
            presenter.Respond(content);
            //---------------Act-------------------
            var result = presenter.Render() as OkObjectResult;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }

        [Fact]
        public void Render_GivenSuccessfulResponse_WithConversionFunction_ShouldReturnOkResultWithConvertedContent()
        {
            //---------------Arrange-------------------
            var content = new DomainObject
            {
                Name = "Travis",
                Age = 38,
                FavoriteColor = "Black"
            };
            Func<DomainObject, object> func = (domainEntity) => new OtherDomainObject
            {
                StatedName = domainEntity.Name,    
                StatedAge = domainEntity.Age
            };
            var presenter = CreatePresenter(func);
            presenter.Respond(content);
            //---------------Act-------------------
            var result = presenter.Render() as OkObjectResult;
            //---------------Assert-------------------
            var expected = new OtherDomainObject
            {
                StatedName = "Travis",
                StatedAge = 38
            };
            result.Value.Should().BeEquivalentTo(expected);
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

        private SuccessOrErrorRestfulPresenter<DomainObject, ErrorOutput> CreatePresenter(
            Func<DomainObject, object> func)
        {
            return new SuccessOrErrorRestfulPresenter<DomainObject, ErrorOutput>(func);
        }

        private SuccessOrErrorRestfulPresenter<DomainObject, ErrorOutput> CreatePresenter()
        {
            return new SuccessOrErrorRestfulPresenter<DomainObject, ErrorOutput>();
        }
    }
}