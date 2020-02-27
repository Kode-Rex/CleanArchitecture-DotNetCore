using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Saga
{
    internal class AsyncActionToSagaStep<TContext> : ISagaStep<TContext>
    {
        private readonly Func<TContext, Task> _action;

        public AsyncActionToSagaStep(Func<TContext, Task> action)
        {
            _action = action;
        }

        public async Task<TContext> Run(TContext context)
        {
            await _action.Invoke(context);
            return context;
        }
    }
}