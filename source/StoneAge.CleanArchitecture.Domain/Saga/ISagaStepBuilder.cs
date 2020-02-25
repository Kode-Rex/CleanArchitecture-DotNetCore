using StoneAge.CleanArchitecture.Domain.Messages;
using System;

namespace StoneAge.CleanArchitecture.Domain.Saga
{
    public interface ISagaStepBuilder<TContext> where TContext : class
    {
        ISagaStepBuilder<TContext> Using_Step(ISagaStep<TContext> step);
        ISagaStepBuilder<TContext> Using_Step(Action<TContext> internalAction);
        ISagaStepBuilder<TContext> With_Roll_Back_Action_On_Error(Action<TContext> action);
        IRunSaga<TContext> With_Finish_Actions(Action<TContext> successAction, Action<ErrorOutput> errorAction);
        IRunSaga<TContext> With_Finish_Action(Action<TContext> successAction);
    }
}