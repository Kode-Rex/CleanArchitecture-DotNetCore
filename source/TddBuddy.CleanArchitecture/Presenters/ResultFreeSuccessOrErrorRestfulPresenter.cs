using Microsoft.AspNetCore.Mvc;
using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.HttpResponses;

namespace TddBuddy.CleanArchitecture.Presenters
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