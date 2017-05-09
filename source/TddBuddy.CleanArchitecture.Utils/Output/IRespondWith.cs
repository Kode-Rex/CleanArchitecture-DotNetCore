namespace TddBuddy.CleanArchitecture.Utils.Output
{
    public interface IRespondWith<in T>
    {
        void Respond(T output);
    }
}