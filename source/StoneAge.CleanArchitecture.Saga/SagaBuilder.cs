using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Saga
{
    public class SagaBuilder<TContext> : ISagaBuilder<TContext>, 
                                         ISagaStepBuilder<TContext> where TContext : class
    {
        public TContext Context { get; set; }
        public List<SagaStepContainer<TContext>> Steps { get; }

        public SagaBuilder()
        {
            Steps = new List<SagaStepContainer<TContext>>();
        }

        public ISagaStepBuilder<TContext> With_Context_State(TContext context)
        {
            Context = context;
            return this;
        }

        public ISagaStepBuilder<TContext> Using_Step(ISagaStep<TContext> step)
        {
            Steps.Add(new SagaStepContainer<TContext>(step));
            
            return this;
        }

        public ISagaStepBuilder<TContext> Using_Step(Func<TContext, Task> internalAction)
        {
            var actionStep = new AsyncActionToSagaStep<TContext>(internalAction);
            Steps.Add(new SagaStepContainer<TContext>(actionStep));
            return this;
        }

        public ISagaStepBuilder<TContext> Using_Step(Action<TContext> internalAction)
        {
            var actionStep = new ActionToSagaStep<TContext>(internalAction);
            Steps.Add(new SagaStepContainer<TContext>(actionStep));
            return this;
        }

        public ISagaStepBuilder<TContext> With_Roll_Back_Action_On_Error(Action<TContext> compensateAction)
        {
            if (Steps.Any())
            {
                var lastStep = Steps.Last();
                lastStep.OnErrorBehavior = ErrorBehavior.Terminate;
                lastStep.CompensateAction = compensateAction;
            }

            return this;
        }

        public IRunSaga<TContext> With_Finish_Actions(Action<TContext> successAction, Action<ErrorOutput> errorAction)
        {
            return new Saga<TContext>(Context, Steps, successAction, errorAction);
        }

        public IRunSaga<TContext> With_Finish_Action(Action<TContext> successAction)
        {
            return new Saga<TContext>(Context, Steps, successAction, (ErrorOutput err)=> { });
        }
    }
}