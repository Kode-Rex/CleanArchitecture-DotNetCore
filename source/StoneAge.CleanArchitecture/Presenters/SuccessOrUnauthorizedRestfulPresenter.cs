using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class SuccessOrUnauthorizedRestfulPresenter<TSuccess> : GenericRestfulPresenter, IRespondWithResultOnSuccessHttpStatusCodeOnError<TSuccess>
        where TSuccess : class
    {
        public void Respond()
        {
            RespondWith(new UnauthorizedResult());
        }

        public void Respond(TSuccess output)
        {
            RespondWith(new OkObjectResult(output));
        }
    }
}