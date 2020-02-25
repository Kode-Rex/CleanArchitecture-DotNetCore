using FluentAssertions;
using NUnit.Framework;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Presenters;
using StoneAge.CleanArchitecture.Tests.Saga.Steps;
using StoneAge.CleanArchitecture.Tests.Saga.UseCases;

namespace StoneAge.CleanArchitecture.Tests.Saga
{
    [TestFixture]
    public class SagaTests
    {
        [Test]
        public void When_No_Errors_Expect_Sum_Of_Two_Numbers_Plus_10()
        {
            // arrange
            var input = new TestInput
            {
                a = 10,
                b = 25
            };
            var addTask = new AddStep();
            var plusTenTask = new PlusTenStep();
            var presenter = new PropertyPresenter<TestResult, ErrorOutput>();
            var sut = new TestUseCase(addTask, plusTenTask);
            // act
            sut.Execute(input, presenter);
            // assert
            var expected = new TestResult
            {
                Result = 45
            };
            presenter.SuccessContent.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void When_Errors_Expect_Sum_Of_Two_Numbers_Plus_10()
        {
            // arrange
            var input = new TestInput
            {
                a = 10,
                b = 25
            };
            var addTask = new AddStep();
            var errorTask = new ErrorStep();
            var presenter = new PropertyPresenter<TestResult, ErrorOutput>();
            var sut = new TestUseCaseError(addTask, errorTask);
            // act
            sut.Execute(input, presenter);
            // assert
            var expected = new ErrorOutput("Error on step 2");
            presenter.ErrorContent.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void When_Inner_UseCase_Expect_65()
        {
            // arrange
            var input = new TestInput
            {
                a = 10,
                b = 25
            };
            var addTask = new AddStep();
            var plusTenTask = new PlusTenStep();

            var presenter = new PropertyPresenter<TestResult, ErrorOutput>();
            var innerUseCase = new TestUseCase(addTask, plusTenTask);
            var sut = new TestUseCaseWithInnerUseCase(innerUseCase, addTask, plusTenTask);
            // act
            sut.Execute(input, presenter);
            // assert
            var expected = new TestResult
            {
                Result = 65
            };
            presenter.SuccessContent.Should().BeEquivalentTo(expected);
        }

        // todo : I need to get instance from DI from startup, then add in my specific things like task?
        // https://stackoverflow.com/questions/42221895/how-to-get-an-instance-of-iserviceprovider-in-net-core
        // then use the command line stuff to add in new task specific to the workflow in quetion
        [Test]
        public void When_Using_DI_Expect_135()
        {
            // arrange
            var input = new TestInput
            {
                a = 10,
                b = 25
            };
            var myRepository = new MathOperations();
            var addTask = new AddStep();
            var addWithDiTask = new AddStepWithDi(myRepository);

            var presenter = new PropertyPresenter<TestResult, ErrorOutput>();

            var sut = new TestUseCaseDi(addTask, addWithDiTask);
            // act
            sut.Execute(input, presenter);
            // assert
            var expected = new TestResult
            {
                Result = 135
            };
            presenter.SuccessContent.Should().BeEquivalentTo(expected);
        }
    }
}
