using System;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain.Presenters
{
    public class InterUseCasePresenter<TSuccessOuter, TSuccessInner, TError> : IRespondWithSuccessOrError<TSuccessInner, TError>
    {
        private TError _errors;
        private TSuccessInner _output;

        public void Respond(TError output)
        {
            _errors = output;
        }

        public void Respond(TSuccessInner output)
        {
            _output = output;
        }

        public void Render(Func<TSuccessInner, TSuccessOuter> conversionFunc, IRespondWithSuccessOrError<TSuccessOuter, TError> presenter)
        {
            if (_errors != null)
            {
                presenter.Respond(_errors);
                return;
            }

            var conversionResult = conversionFunc.Invoke(_output);
            presenter.Respond(conversionResult);
        }
    }
}
