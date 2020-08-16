
using StoneAge.CleanArchitecture.Domain;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.Saga;
using StoneAge.CleanArchitecture.Tests.Saga.Steps;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.UseCases
{
    public class AddTwoNumbersTerminateOnError : IUseCase<TestInput, TestResult>
    {
        public void Execute(TestInput inputTo, IRespondWithSuccessOrError<TestResult, ErrorOutput> presenter)
        {
            var context = new TestContext
            {
                Value1 = inputTo.a,
                Value2 = inputTo.b
            };

            var sagaWithCompenstate = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(new Func<TestContext, Task>(async (ctx) =>
                            {
                                await Add_Numbers(ctx);
                                ctx.AddError("error adding two numbers");
                            }))
                            .With_Error_Behavior(Domain.Saga.ErrorBehavior.Terminate) // could be Terminate so saga halts at this step
                            .Using_Step(new Func<TestContext, Task>(async (ctx) =>
                            {
                                await Add_Numbers(ctx);
                            }))
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
        }

        private async Task Add_Numbers(TestContext ctx)
        {
            await Task.FromResult(ctx.Result = ctx.Value1 + ctx.Value2);
        }
    }
}
