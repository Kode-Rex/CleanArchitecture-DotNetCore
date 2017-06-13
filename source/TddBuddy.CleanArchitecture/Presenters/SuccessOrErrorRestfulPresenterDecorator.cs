using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;

namespace TddBuddy.CleanArchitecture.Presenters
{
    public class SuccessOrErrorRestfulPresenterDecorator<TSuccess> : IRespondWithSuccessOrError<TSuccess, ErrorOutputMessage>
        where TSuccess : class
    {
        private readonly GenericRestfulPresenter<TSuccess, List<string>> _restfulPresenter;

        public SuccessOrErrorRestfulPresenterDecorator(GenericRestfulPresenter<TSuccess, List<string>> restfulPresenter)
        {
            _restfulPresenter = restfulPresenter;
        }

        public void Respond(ErrorOutputMessage output)
        {
            _restfulPresenter.RespondWithUnprocessableEntity(output.Errors);
        }

        public void Respond(TSuccess output)
        {
            _restfulPresenter.RespondWithOk(output);
        }
    }
}