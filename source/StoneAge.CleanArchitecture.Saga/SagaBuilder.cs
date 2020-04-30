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
            Context = default;
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

        public ISagaStepBuilder<TContext> With_Error_Behavior(ErrorBehavior errorBehavior)
        {
            if (Steps.Any())
            {
                var lastStep = Steps.Last();
                lastStep.OnErrorBehavior = errorBehavior;
            }

            return this;
        }

        // todo : add .Halt_On_Condition((ctx)=>{ });

        public ISagaStepBuilder<TContext> With_Roll_Back_Action_On_Error(Action<TContext> compensateAction)
        {
            if (Steps.Any())
            {
                var lastStep = Steps.Last();
                lastStep.CompensateAction = compensateAction;
            }

            return this;
        }

        public ISagaStepBuilder<TContext> With_Roll_Back_Action_On_Error(Func<TContext, Task> compensateFunc)
        {
            if (Steps.Any())
            {
                var lastStep = Steps.Last();
                lastStep.CompensateFunc = compensateFunc;
            }

            return this;
        }

        public ISagaStepBuilder<TContext> With_Roll_Back_Action_On_Error(ISagaStep<TContext> compensateStep)
        {
            if (Steps.Any())
            {
                var lastStep = Steps.Last();
                lastStep.CompensateStep = compensateStep;
            }

            return this;
        }

        public IRunSaga<TContext> With_Finish_Actions(Func<TContext, Task> successFunc, Func<ErrorOutput, Task> errorFunc)
        {
            return new Saga<TContext>(Context, Steps, null, null, successFunc, errorFunc);
        }

        public IRunSaga<TContext> With_Finish_Action(Func<TContext, Task> successFunc)
        {
            return new Saga<TContext>(Context, Steps, null, null, successFunc, null);
        }

        public IRunSaga<TContext> With_Finish_Actions(Action<TContext> successAction, Action<ErrorOutput> errorAction)
        {
            return new Saga<TContext>(Context, Steps, successAction, errorAction, null, null);
        }

        // todo : add ctx to err
        public IRunSaga<TContext> With_Finish_Action(Action<TContext> successAction)
        {
            return new Saga<TContext>(Context, Steps, successAction, (ErrorOutput err)=> { }, null, null);
        }


    }
}