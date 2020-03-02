using StoneAge.CleanArchitecture.Domain;
using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.Saga;
using StoneAge.CleanArchitecture.Tests.Saga.Steps;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.UseCases
{

    public class AddTwoNumbersThenAddTen : IUseCase<TestInput, TestResult>
    {
        private readonly AddStep _addTask;
        private readonly PlusTenStep _plusTenTask;

        public AddTwoNumbersThenAddTen(AddStep addTask, PlusTenStep plusTenTask)
        {
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

            // using inline actions
            var sagaWithActions = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step((ctx) =>
                            {
                                ctx.Result = ctx.Value1 + ctx.Value2;
                            })
                            .Using_Step((ctx) =>
                            {
                                ctx.Result += 10;
                            })
                            .With_Finish_Action((ctx) =>
                            {
                                presenter.Respond(new TestResult
                                {
                                    Result = ctx.Result
                                });
                            });

            // using local methods to wrap actions
            var sagaWithActionWrappingMethods = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step((ctx) =>
                            {
                                AddTwoNumbers(ctx);
                            })
                            .Using_Step((ctx) =>
                            {
                                AddTen(ctx);
                            })
                            .With_Finish_Action((ctx) =>
                            {
                                presenter.Respond(new TestResult
                                {
                                    Result = ctx.Result
                                });
                            });

            // using inline async actions
            var sagaWithAsyncActions = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(new Func<TestContext, Task>(async (ctx) =>
                            {
                                await Task.FromResult(ctx.Result = ctx.Value1 + ctx.Value2);
                            }))
                            .Using_Step(new Func<TestContext, Task>(async (ctx) =>
                            {
                                await Task.FromResult(ctx.Result += 10);
                            }))
                            .With_Finish_Action((ctx) =>
                            {
                                presenter.Respond(new TestResult
                                {
                                    Result = ctx.Result
                                });
                            });

            // using Steps
            var sagaWithSteps = new SagaBuilder<TestContext>()
                            .With_Context_State(context)
                            .Using_Step(_addTask)
                            .Using_Step(_plusTenTask)
                            .With_Finish_Action((ctx) =>
                            {
                                presenter.Respond(new TestResult
                                {
                                    Result = ctx.Result
                                });
                            });

            sagaWithActions.Run();
            //sagaWithActionWrappingMethods.Run();
            //sagaWithAsyncActions.Run();
            //sagaWithSteps.Run();
        }

        private static void AddTen(TestContext ctx) => ctx.Result += 10;
        private static void AddTwoNumbers(TestContext ctx) => ctx.Result = ctx.Value1 + ctx.Value2;
    }
}
