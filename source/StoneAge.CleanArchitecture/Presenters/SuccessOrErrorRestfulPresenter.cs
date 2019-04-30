using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.HttpResponses;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class SuccessOrErrorRestfulPresenter<TSuccess, TError> : GenericRestfulPresenter, IRespondWithSuccessOrError<TSuccess, TError>
        where TSuccess : class
        where TError : class
    {
        public void Respond(TError output)
        {
            RespondWith(new UnprocessableEntityResult<TError>(output));
        }

        public void Respond(TSuccess output)
        {
            RespondWith(new OkObjectResult(output));
        }
    }
}