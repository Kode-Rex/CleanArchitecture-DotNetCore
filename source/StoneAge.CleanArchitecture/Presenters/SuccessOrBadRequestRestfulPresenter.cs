using Microsoft.AspNetCore.Mvc;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class SuccessOrBadRequestRestfulPresenter<TSuccess> : GenericRestfulPresenter, IRespondWithResultOnSuccessHttpStatusCodeOnError<TSuccess>
        where TSuccess : class
    {
        public void Respond()
        {
            RespondWith(new BadRequestResult());
        }

        public void Respond(TSuccess output)
        {
            RespondWith(new OkObjectResult(output));
        }
    }
}