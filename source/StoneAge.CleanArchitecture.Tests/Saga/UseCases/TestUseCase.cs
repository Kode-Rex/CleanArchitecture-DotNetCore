using StoneAge.CleanArchitecture.Domain;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.Saga;
using StoneAge.CleanArchitecture.Tests.Saga.Steps;

namespace StoneAge.CleanArchitecture.Tests.Saga.UseCases
{

    public class TestUseCase : IUseCase<TestInput, TestResult>
    {
        private readonly AddStep _addTask;
        private readonly PlusTenStep _plusTenTask;

        public TestUseCase(AddStep addTask, PlusTenStep plusTenTask)
        {
            _addTask = addTask;
            _plusTenTask = plusTenTask;
        }

        public void Execute(TestInput inputTo, IRespondWithSuccessOrError<TestResult, ErrorOutput> presenter)
        {
            var context = new TestContext
            {
                a = inputTo.a,
                b = inputTo.b
            };

            // todo : maybe a mini Di configuration so I can decide how to create?
            var workflowResult = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(_addTask)
                            .Using_Step(_plusTenTask)
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
