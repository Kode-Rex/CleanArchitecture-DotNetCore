using TddBuddy.CleanArchitecture.Utils.Output;

namespace TddBuddy.CleanArchitecture.Utils.Presenters
{
    public class PropertyPresenter<TSuccess, TError> : IRespondWithSuccessOrError<TSuccess, TError>
    {
        public TError ErrorContent { get; private set; }
        public TSuccess SuccessContent { get; private set; }

        public void Respond(TError output)
        {
            ErrorContent = output;
        }

        public void Respond(TSuccess output)
        {
            SuccessContent = output;
        }

        public bool IsErrorResponse()
        {
            return ErrorContent != null;
        }
    }
}
