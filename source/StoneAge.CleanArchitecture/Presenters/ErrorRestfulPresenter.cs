using StoneAge.CleanArchitecture.Domain.Output;
using StoneAge.CleanArchitecture.HttpResponses;

namespace StoneAge.CleanArchitecture.Presenters
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