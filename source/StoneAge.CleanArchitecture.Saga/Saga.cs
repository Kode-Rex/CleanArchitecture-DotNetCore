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
        private readonly Func<TContext, Task> _completeFunc;
        private readonly Func<ErrorOutput, Task> _errorFunc;

        public Saga(TContext context, 
                    List<SagaStepContainer<TContext>> steps, 
                    Action<TContext> completeAction,
                    Action<ErrorOutput> errorAction,
                    Func<TContext, Task> completeFunc,
                    Func<ErrorOutput, Task> errorFunc)
        {
            Steps = steps;
            Context = context;
            _completeAction = completeAction;
            _errorAction = errorAction;
            _completeFunc = completeFunc;
            _errorFunc = errorFunc;
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
                await Invoke_Error_Handling(result);
                return result;
            }

            await Invoke_Success_Handling();

            return result;
        }

        private async Task Invoke_Success_Handling()
        {
            if (_completeAction != null)
            {
                _completeAction.Invoke(Context);
            }
            else if (_completeFunc != null)
            {
                await _completeFunc.Invoke(Context);
            }
        }

        private async Task Invoke_Error_Handling(SagaExecutionContext<TContext> result)
        {
            if (_errorAction != null)
            {
                _errorAction.Invoke(result.Errors);
            }
            else if (_errorFunc != null)
            {
                await _errorFunc.Invoke(result.Errors);
            }
        }

        private static void Log_Execution_Error(SagaExecutionContext<TContext> result, Exception e) => result.AddError(e);
    }
}
