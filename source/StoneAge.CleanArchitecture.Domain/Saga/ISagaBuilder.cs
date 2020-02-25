namespace StoneAge.CleanArchitecture.Domain.Saga
{
    public interface ISagaBuilder<TContext> where TContext : class
    {
        ISagaStepBuilder<TContext> With_Context_State(TContext context);
    }
}
