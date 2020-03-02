using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class AddTenErrorStep : ISagaStep<TestContext>
    {
        public Task<TestContext> Run(TestContext context)
        {
            context.Result += 10;
            throw new Exception("Error in task");
        }
    }
}
