using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Saga
{
    internal class ActionToSagaStep<TContext> : ISagaStep<TContext>
    {
        private readonly Action<TContext> _action;

        public ActionToSagaStep(Action<TContext> action)
        {
            _action = action;
        }

        public Task<TContext> Run(TContext context)
        {
            _action.Invoke(context);
            return Task.FromResult(context);
        }
    }
}