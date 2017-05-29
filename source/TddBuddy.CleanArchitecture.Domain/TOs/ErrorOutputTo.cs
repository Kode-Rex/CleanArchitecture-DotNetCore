using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TddBuddy.CleanArchitecture.Domain.TOs
{
    public class ErrorOutputTo
    {
        public List<string> Errors { get; }

        [JsonIgnore]
        public bool HasErrors => Errors.Any();

        public ErrorOutputTo()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddErrors(List<string> errors)
        {
            Errors.AddRange(errors);
        }
    }
}