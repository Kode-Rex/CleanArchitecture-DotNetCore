using TddBuddy.CleanArchitecture.Utils.Output;

namespace TddBuddy.CleanArchitecture.Utils.Presenters
{
    public class ErrorRestfulPresenter<TError> : GenericRestfulPresenter<object, TError>, IRespondWith<TError>
         where TError : class
    {
        public ErrorRestfulPresenter()
        {
            DefaultResponse(presenter => presenter.RespondWithOk());
        }

        public void Respond(TError output)
        {
            RespondWithUnprocessableEntity(output);
        }
    }
}