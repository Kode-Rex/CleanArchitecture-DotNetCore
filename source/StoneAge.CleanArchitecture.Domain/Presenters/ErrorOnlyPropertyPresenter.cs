using System;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain.Presenters
{
    public class ErrorOnlyPropertyPresenter<TError> : IRespondWithNoResultSuccessOrError<TError> where TError : class
    {
        public TError Output { get; private set; }

        public void Respond(TError output)
        {
            Output = output;
        }

        public void Respond()
        {
            // do nothing
        }
    }
}
