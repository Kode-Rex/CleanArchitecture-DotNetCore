using StoneAge.CleanArchitecture.Domain;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.Saga;
using StoneAge.CleanArchitecture.Tests.Saga.Steps;

namespace StoneAge.CleanArchitecture.Tests.Saga.UseCases
{
    public class AddTwoNumbersThenAddTenThrowsError : IUseCase<TestInput, TestResult>
    {
        private readonly AddTenErrorStep _errorTask;
        private readonly AddStep _addTask;

        public AddTwoNumbersThenAddTenThrowsError(AddStep addTask, AddTenErrorStep errorTask)
        {
            _errorTask = errorTask;
            _addTask = addTask;
        }

        public void Execute(TestInput inputTo, IRespondWithSuccessOrError<TestResult, ErrorOutput> presenter)
        {
            var context = new TestContext
            {
                Value1 = inputTo.a,
                Value2 = inputTo.b
            };

            var sagaWithoutCompenstate = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(_addTask)
                            .Using_Step(_errorTask)
                            .With_Finish_Actions((ctx) =>
                            {
                                presenter.Respond(new TestResult
                                {
                                    Result = ctx.Result
                                });
                            },
                            (err)=>
                            {
                                presenter.Respond(err);
                            });

            var sagaWithCompenstate = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(_addTask)
                            .With_Error_Behavior(Domain.Saga.ErrorBehavior.Continue) // could be Terminate so saga halts at this step
                            .Using_Step(_errorTask)
                            .With_Roll_Back_Action_On_Error((ctx) =>
                            {
                                ctx.Result -= 10;
                            })
                            .With_Finish_Actions((ctx) =>
                            {
                                presenter.Respond(new TestResult
                                {
                                    Result = ctx.Result
                                });
                            },
                            (err) =>
                            {
                                presenter.Respond(err);
                            });

            sagaWithCompenstate.Run();
            //sagaWithoutCompenstate.Run();

        }
    }
}
