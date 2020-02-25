using StoneAge.CleanArchitecture.Domain;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.Saga;
using StoneAge.CleanArchitecture.Tests.Saga.Steps;

namespace StoneAge.CleanArchitecture.Tests.Saga.UseCases
{

    public class TestUseCaseError : IUseCase<TestInput, TestResult>
    {
        private readonly ErrorStep _errorTask;
        private readonly AddStep _addTask;

        public TestUseCaseError(AddStep addTask, ErrorStep errorTask)
        {
            _errorTask = errorTask;
            _addTask = addTask;
        }

        public void Execute(TestInput inputTo, IRespondWithSuccessOrError<TestResult, ErrorOutput> presenter)
        {
            var context = new TestContext
            {
                a = inputTo.a,
                b = inputTo.b
            };

            var workflowResult = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(_addTask)
                            .Using_Step(_errorTask)
                            .With_Roll_Back_Action_On_Error((ctx) =>
                            {
                                presenter.Respond(new ErrorOutput("Error on step 2"));
                            })
                            .With_Finish_Action((ctx) =>
                            {
                                presenter.Respond(new TestResult
                                {
                                    Result = ctx.c
                                });
                            })
                            .Run();
        }
    }
}
