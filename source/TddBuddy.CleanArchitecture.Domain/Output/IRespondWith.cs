namespace TddBuddy.CleanArchitecture.Domain.Output
{
    public interface IRespondWith<in T>
    {
        void Respond(T output);
    }
}