using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Presenters;
using StoneAge.CleanArchitecture.Tests.Presenters.TestObjects;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class SuccessOrUnauthorizedRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenSuccessfulResponse_ShouldReturnOkResultWithContent()
        {
            //---------------Arrange-------------------
            var content = new DomainObject
            {
                Name = "Trev",
                Age = 28,
                FavoriteColor = "Pink"
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
                FavoriteColor = "Pink"
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
            var presenter = CreatePresenter();
            presenter.Respond();
            //---------------Act-------------------
            var result = presenter.Render() as UnauthorizedResult;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        private SuccessOrUnauthorizedRestfulPresenter<DomainObject> CreatePresenter()
        {
            return new SuccessOrUnauthorizedRestfulPresenter<DomainObject>();
        }

        private SuccessOrUnauthorizedRestfulPresenter<DomainObject> CreatePresenter(Func<DomainObject, object> func)
        {
            return new SuccessOrUnauthorizedRestfulPresenter<DomainObject>(func);
        }
    }
}
