using CleanArchitecture.Utils.Output;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Utils.Presenters
{
    public class ErrorRestfulPresenter<TError> : GenericRestfulPresenter<object, TError>, IRespondWith<TError>
         where TError : class
    {
        public ErrorRestfulPresenter(Controller controller) : base(controller)
        {
            DefaultResponse(presenter => presenter.RespondWithOk());
        }

        public void Respond(TError output)
        {
            RespondWithUnprocessableEntity(output);
        }
    }
}