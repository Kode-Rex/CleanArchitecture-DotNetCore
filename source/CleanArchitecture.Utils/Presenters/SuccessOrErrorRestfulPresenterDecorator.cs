using System.Collections.Generic;
using CleanArchitecture.Utils.Output;
using CleanArchitecture.Utils.TOs;

namespace CleanArchitecture.Utils.Presenters
{
    public class SuccessOrErrorRestfulPresenterDecorator<TSuccess> : IRespondWithSuccessOrError<TSuccess, ErrorOutputTo>
        where TSuccess : class
    {
        private readonly GenericRestfulPresenter<TSuccess, List<string>> _restfulPresenter;

        public SuccessOrErrorRestfulPresenterDecorator(GenericRestfulPresenter<TSuccess, List<string>> restfulPresenter)
        {
            _restfulPresenter = restfulPresenter;
        }

        public void Respond(ErrorOutputTo output)
        {
            _restfulPresenter.RespondWithUnprocessableEntity(output.Errors);
        }

        public void Respond(TSuccess output)
        {
            _restfulPresenter.RespondWithOk(output);
        }
    }
}