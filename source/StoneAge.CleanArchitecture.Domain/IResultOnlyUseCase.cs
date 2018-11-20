using StoneAge.CleanArchitecture.Domain.Output;

namespace StoneAge.CleanArchitecture.Domain
{
    public interface IResultOnlyUseCase<in TInputMessage, out TItOutputMessage>
    {
        void Execute(TInputMessage inputTo, IRespondWith<TItOutputMessage> presenter);
    }
}
