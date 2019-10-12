using System;
using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class SuccessOrBadRequestRestfulPresenter<TSuccess> : GenericRestfulPresenter, IRespondWithResultOnSuccessHttpStatusCodeOnError<TSuccess>
        where TSuccess : class
    {
        private readonly Func<TSuccess, object> _boundaryFunc;

        public SuccessOrBadRequestRestfulPresenter() { }

        public SuccessOrBadRequestRestfulPresenter(Func<TSuccess, object> boundaryFunc)
        {
            _boundaryFunc = boundaryFunc;
        }

        public void Respond()
        {
            RespondWith(new BadRequestResult());
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