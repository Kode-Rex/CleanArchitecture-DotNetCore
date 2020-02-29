using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Saga
{
    public class SagaStepContainer<TContext> : ISagaStep<TContext> where TContext : class
    {
        public ErrorBehavior OnErrorBehavior { get; internal set; }
        public Action<TContext> CompensateAction { private get; set; }
        public Func<TContext, Task> CompensateFunc { private get; set; }
        public ISagaStep<TContext> Step { get; }
        
        public SagaStepContainer(ISagaStep<TContext> step)
        {
            Step = step;
        }

        public async Task<TContext> Run(TContext context)
        {
            return await Step.Run(context);
        }

        public async Task<TContext> Compensate(TContext context)
        {
            if(CompensateAction != null)
            {
                CompensateAction.Invoke(context);

            }else if(CompensateFunc != null)
            {
                await CompensateFunc.Invoke(context);
            }

            return await Task.FromResult(context);
        }
    }
}