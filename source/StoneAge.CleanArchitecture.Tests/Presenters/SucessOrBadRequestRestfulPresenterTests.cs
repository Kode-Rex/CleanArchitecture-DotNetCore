using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Presenters;
using StoneAge.CleanArchitecture.Tests.Presenters.TestObjects;
using Xunit;

namespace StoneAge.CleanArchitecture.Tests.Presenters
{
    public class SucessOrBadRequestRestfulPresenterTests
    {
        [Fact]
        public void Render_GivenSuccessfulResponse_ShouldReturnOkResultWithContent()
        {
            //---------------Arrange-------------------
            var content = new DomainObject
            {
                Name = "Travis",
                Age = 38,
                FavoriteColor = "Black"
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
            var presenter = CreatePresenter();
            presenter.Respond();
            //---------------Act-------------------
            var result = presenter.Render() as BadRequestResult;
            //---------------Assert-------------------
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        private SuccessOrBadRequestRestfulPresenter<DomainObject> CreatePresenter()
        {
            return new SuccessOrBadRequestRestfulPresenter<DomainObject>();
        }

        private SuccessOrBadRequestRestfulPresenter<DomainObject> CreatePresenter(Func<DomainObject, object> func)
        {
            return new SuccessOrBadRequestRestfulPresenter<DomainObject>(func);
        }
    }
}
