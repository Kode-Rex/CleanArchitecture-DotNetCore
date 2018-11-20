using Microsoft.AspNetCore.Mvc;

namespace StoneAge.CleanArchitecture.Presenters
{
    public class SuccessOrUnauthorizedRestfulPresenter<TSuccess> : GenericRestfulPresenter, IRespondWith<TSuccess>
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