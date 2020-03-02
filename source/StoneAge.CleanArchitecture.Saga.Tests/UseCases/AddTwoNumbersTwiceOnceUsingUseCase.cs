using StoneAge.CleanArchitecture.Domain;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.Domain.Presenters;
using StoneAge.CleanArchitecture.Saga;
using StoneAge.CleanArchitecture.Tests.Saga.Steps;
using System;

namespace StoneAge.CleanArchitecture.Tests.Saga.UseCases
{
    public class AddTwoNumbersTwiceOnceUsingUseCase : IUseCase<TestInput, TestResult>
    {
        private readonly AddTwoNumbersThenAddTen _useCase;
        private readonly AddStep _addTask;
        private readonly PlusTenStep _plusTenTask;

        public AddTwoNumbersTwiceOnceUsingUseCase(AddTwoNumbersThenAddTen useCase, AddStep addTask, PlusTenStep plusTenTask)
        {
            _useCase = useCase;
            _addTask = addTask;
            _plusTenTask = plusTenTask;
        }

        public void Execute(TestInput inputTo, IRespondWithSuccessOrError<TestResult, ErrorOutput> presenter)
        {
            var context = new TestContext
            {
                Value1 = inputTo.a,
                Value2 = inputTo.b
            };

            var workflowResult = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(_addTask)
                            .Using_Step(_plusTenTask)
                            .Using_Step((ctx) =>
                            {
                                var input = new TestInput
                                {
                                    a = ctx.Value1,
                                    b = ctx.Result
                                };
                                var propertyPresenter = new PropertyPresenter<TestResult, ErrorOutput>();
                                _useCase.Execute(input, propertyPresenter);
                                if (propertyPresenter.IsErrorResponse())
                                {
                                    throw new Exception();
                                }
                                ctx.Result = propertyPresenter.SuccessContent.Result;
                            })
                            .With_Finish_Actions(Respond_With_Success(presenter), Respond_With_Error(presenter))
                            .Run();
        }

        private static Action<ErrorOutput> Respond_With_Error(IRespondWithSuccessOrError<TestResult, ErrorOutput> presenter) => (ErrorOutput errors) =>
        {
            presenter.Respond(errors);
        };

        private static Action<TestContext> Respond_With_Success(IRespondWithSuccessOrError<TestResult, ErrorOutput> presenter) => (TestContext ctx) =>
        {
            presenter.Respond(new TestResult
            {
                Result = ctx.Result
            });
        };
    }
}
