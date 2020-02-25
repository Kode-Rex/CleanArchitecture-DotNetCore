using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Saga
{
    public class SagaStepContainer<TContext> : ISagaStep<TContext> where TContext : class
    {
        public ErrorBehavior OnErrorBehavior { get; internal set; }
        public Action<TContext> CompensateAction { get; internal set; }
        public ISagaStep<TContext> Step { get; }
        
        public SagaStepContainer(ISagaStep<TContext> step)
        {
            Step = step;
        }

        public Task<TContext> Run(TContext context)
        {
            return Step.Run(context);
        }
    }
}