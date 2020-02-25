using StoneAge.CleanArchitecture.Domain.Messages;
using System;

namespace StoneAge.CleanArchitecture.Domain.Saga
{
    public class SagaExecutionContext<TContext> where TContext : class
    {
        public TContext ExeuctionContext { get; set; }
        public ErrorOutput Errors { get; }

        public SagaExecutionContext()
        {
            Errors = new ErrorOutput();
        }

        public void AddError(Exception e)
        {
            Errors.AddError(e.Message);
        }

        public bool HasErrors()
        {
            return Errors.HasErrors;
        }
    }
}