using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Saga
{
    public class Saga<TContext> : IRunSaga<TContext> where TContext : class
    {
        public TContext Context { get; set; }
        public List<SagaStepContainer<TContext>> Steps { get; }
        private readonly Action<TContext> _completeAction;
        private readonly Action<ErrorOutput> _errorAction;

        public Saga(TContext context, 
                    List<SagaStepContainer<TContext>> steps, 
                    Action<TContext> completeAction,
                    Action<ErrorOutput> errorAction)
        {
            Steps = steps;
            Context = context;
            _completeAction = completeAction;
            _errorAction = errorAction;
        }

        public async Task<SagaExecutionContext<TContext>> Run()
        {
            var result = new SagaExecutionContext<TContext>();
            foreach (var step in Steps)
            {
                try
                {
                    Context = await step.Run(Context);
                }
                catch (Exception e)
                {
                    Log_Execution_Error(result, e);
                    await step.Compensate(Context);

                    if (step.OnErrorBehavior == ErrorBehavior.Terminate)
                    {
                        break;
                    }
                }
            }

            if (result.HasErrors())
            {
                _errorAction.Invoke(result.Errors);
                return result;
            }

            _completeAction.Invoke(Context);
            return result;
        }

        private static void Log_Execution_Error(SagaExecutionContext<TContext> result, Exception e) => result.AddError(e);
    }
}
