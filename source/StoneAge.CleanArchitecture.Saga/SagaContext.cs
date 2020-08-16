using StoneAge.CleanArchitecture.Domain.Messages;

namespace StoneAge.CleanArchitecture.Saga
{
    public abstract class SagaContext
    {
        public ErrorOutput Errors { get; private set; }

        public SagaContext()
        {
            Errors = new ErrorOutput();
        }

        public bool HasErrors() 
        {
            return Errors?.HasErrors == true;
        }

        public void AddErrors(ErrorOutput errors)
        {
            Errors = errors;
        }

        public void AddError(string error)
        {
            Errors.AddError(error);
        }
    }
}