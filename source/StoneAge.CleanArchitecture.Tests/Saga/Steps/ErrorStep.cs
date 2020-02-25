using StoneAge.CleanArchitecture.Domain.Saga;
using System;
using System.Threading.Tasks;

namespace StoneAge.CleanArchitecture.Tests.Saga.Steps
{
    public class ErrorStep : ISagaStep<TestContext>
    {
        public Task<TestContext> Run(TestContext context)
        {
            throw new Exception("Error in task");
        }
    }
}
