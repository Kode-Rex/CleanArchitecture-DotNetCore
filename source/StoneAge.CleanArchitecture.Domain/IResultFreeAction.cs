using StoneAge.CleanArchitecture.Domain.Messages;
using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IResultFreeAction
    {
        void Execute(IRespondWithNoResultSuccessOrError<ErrorOutput> presenter);
    }
}
