using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.HttpResponses;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class ResultFreeSuccessOrErrorRestfulPresenter<TError> : GenericRestfulPresenter, IRespondWithNoResultSuccessOrError<TError>
        where TError : class
    {
        public void Respond(TError output)
        {
            RespondWith(new UnprocessasbleEntityResult<TError>(output));
        }

        public void Respond()
        {
            RespondWith(new OkResult());
        }
    }
}