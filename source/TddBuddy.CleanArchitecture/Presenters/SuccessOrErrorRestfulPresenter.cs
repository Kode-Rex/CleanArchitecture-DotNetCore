using Microsoft.AspNetCore.Mvc;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.HttpResponses;

namespace TddBuddy.CleanArchitecture.Presenters
{
    public class SuccessOrErrorRestfulPresenter<TSuccess, TError> : GenericRestfulPresenter, IRespondWithSuccessOrError<TSuccess, TError>
        where TSuccess : class
        where TError : class
    {
        public void Respond(TError output)
        {
            RespondWith(new UnprocessasbleEntityResult<TError>(output));
        }

        public void Respond(TSuccess output)
        {
            RespondWith(new OkObjectResult(output));
        }

        public void Respond()
        {
            RespondWith(new OkResult());
        }
    }
}