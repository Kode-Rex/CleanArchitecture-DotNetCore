using StoneAge.CleanArchitecture.Domain.Messages;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Domain.Saga
{
    public interface ISagaStepBuilder<TContext> where TContext : class
    {
        ISagaStepBuilder<TContext> Using_Step(ISagaStep<TContext> step);
        ISagaStepBuilder<TContext> Using_Step(Action<TContext> internalAction);
        ISagaStepBuilder<TContext> Using_Step(Func<TContext, Task> internalAction);
        ISagaStepBuilder<TContext> With_Error_Behavior(ErrorBehavior errorBehavior);
        ISagaStepBuilder<TContext> With_Roll_Back_Action_On_Error(Action<TContext> action);
        ISagaStepBuilder<TContext> With_Roll_Back_Action_On_Error(Func<TContext, Task> internalAction);
        IRunSaga<TContext> With_Finish_Actions(Action<TContext> successAction, Action<ErrorOutput> errorAction);
        IRunSaga<TContext> With_Finish_Action(Action<TContext> successAction);
    }
}