using TddBuddy.CleanArchitecture.Domain.Output;
using TddBuddy.CleanArchitecture.HttpResponses;

namespace TddBuddy.CleanArchitecture.Presenters
{
    public class ErrorRestfulPresenter<TError> : GenericRestfulPresenter, IRespondWith<TError>
         where TError : class
    {
        public void Respond(TError output)
        {
           RespondWith(new UnprocessasbleEntityResult<TError>(output));
        }
    }
}