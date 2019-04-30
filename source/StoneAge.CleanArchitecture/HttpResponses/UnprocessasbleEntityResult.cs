using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoneAge.CleanArchitecture.HttpResponses
{
    public class UnprocessableEntityResult<T> : ObjectResult
    {
        public UnprocessableEntityResult(T value) : base(value)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}