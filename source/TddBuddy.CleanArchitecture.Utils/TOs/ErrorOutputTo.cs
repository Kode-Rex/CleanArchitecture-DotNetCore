using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TddBuddy.CleanArchitecture.Utils.TOs
{
    public class ErrorOutputTo
    {
        private List<string> Errors { get; }

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

        public List<string> FetchErrors()
        {
            return Errors;
        }
    }
}