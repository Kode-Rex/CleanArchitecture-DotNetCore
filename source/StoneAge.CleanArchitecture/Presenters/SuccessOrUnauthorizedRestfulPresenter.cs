using System;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class SuccessOrUnauthorizedRestfulPresenter<TSuccess> : GenericRestfulPresenter, IRespondWithResultOnSuccessHttpStatusCodeOnError<TSuccess>
        where TSuccess : class
    {
        private readonly Func<TSuccess, object> _boundaryFunc;

        public SuccessOrUnauthorizedRestfulPresenter() { }

        public SuccessOrUnauthorizedRestfulPresenter(Func<TSuccess, object> boundaryFunc)
        {
            _boundaryFunc = boundaryFunc;
        }

        public void Respond()
        {
            RespondWith(new UnauthorizedResult());
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