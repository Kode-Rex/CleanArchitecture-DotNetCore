using System;
using FluentAssertions;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.Domain.Presenters;
using Xunit;

namespace StoneAge.CleanArchitecture.Domain.Tests.Presenter
{
    public class InterUseCasePresenterTests
    {
        [Fact]
        public void Given_There_Are_No_Errors_On_Inner_Execute__Expect_Data_Transfered_To_Outer_Object()
        {
            //---------------Arrange-------------------
            var input = new RandomPersonInput
            {
                MinAge = 19,
                MaxAge = 19
            };
            var useCase = new GenerateRandomPersonUseCase();
            var outerPresenter = new PropertyPresenter<OuterTestObject, ErrorOutput>();
            var innerPresenter = new InterUseCasePresenter<OuterTestObject, InnerTestObject, ErrorOutput>();
            //---------------Act-------------------
            useCase.Execute(input, innerPresenter);
            innerPresenter.Render(x => new OuterTestObject
            {
                Age = x.Age,
                FullName = $"{x.FirstName} {x.LastName}"
            }, outerPresenter);
            //---------------Assert-------------------
            var expected = new OuterTestObject
            {
                FullName = "Test User",
                Age = 19
            };
            outerPresenter.SuccessContent.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Given_There_Are_Errors_On_Inner_Execute__Expect_Errors_Transfered_To_Outer_Object()
        {
            //---------------Arrange-------------------
            var input = new RandomPersonInput
            {
                MinAge = 19,
                MaxAge = 19,
                ForceError = true
            };
            var useCase = new GenerateRandomPersonUseCase();
            var outerPresenter = new PropertyPresenter<OuterTestObject, ErrorOutput>();
            var innerPresenter = new InterUseCasePresenter<OuterTestObject, InnerTestObject, ErrorOutput>();
            //---------------Act-------------------
            useCase.Execute(input, innerPresenter);
            innerPresenter.Render(x => new OuterTestObject
            {
                Age = x.Age,
                FullName = $"{x.FirstName} {x.LastName}"
            }, outerPresenter);
            //---------------Assert-------------------
            var expected = new ErrorOutput();
            expected.AddError("Forced error happened");
            outerPresenter.ErrorContent.Should().BeEquivalentTo(expected);
        }
    }

    public class GenerateRandomPersonUseCase : IUseCase<RandomPersonInput, InnerTestObject>
    {
        public void Execute(RandomPersonInput inputTo, IRespondWithSuccessOrError<InnerTestObject, ErrorOutput> presenter)
        {
            if (inputTo.ForceError)
            {
                var errors = new ErrorOutput();
                errors.AddError("Forced error happened");
                presenter.Respond(errors);
                return;
            }

            var random = new Random(11223);
            presenter.Respond(new InnerTestObject
            {
                Age = random.Next(inputTo.MinAge, inputTo.MaxAge),
                FirstName = "Test",
                LastName = "User"
            });
        }
    }

    public class RandomPersonInput
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool ForceError { get; set; }
    }

    public class InnerTestObject
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class OuterTestObject
    {
        public int Age { get; set; }
        public string FullName { get; set; }
    }
}
