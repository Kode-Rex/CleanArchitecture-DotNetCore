using System;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.HttpResponses;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class SuccessOrErrorRestfulPresenter<TSuccess, TError> : GenericRestfulPresenter, IRespondWithSuccessOrError<TSuccess, TError>
        where TSuccess : class
        where TError : class
    {
        private readonly Func<TSuccess, object> _boundaryFunc;

        public SuccessOrErrorRestfulPresenter() { }

        public SuccessOrErrorRestfulPresenter(Func<TSuccess, object> boundaryFunc)
        {
            _boundaryFunc = boundaryFunc;
        }

        public void Respond(TError output)
        {
            RespondWith(new UnprocessableEntityResult<TError>(output));
        }

        public void Respond(TSuccess output)
        {
            if (_boundaryFunc != null)
            {
                var convertedResult = _boundaryFunc.Invoke(output);
                RespondWith(new OkObjectResult(convertedResult));
                return;
            }

            RespondWith(new OkObjectResult(output));
        }
    }
}