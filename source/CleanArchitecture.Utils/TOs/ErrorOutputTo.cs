using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CleanArchitecture.Utils.TOs
{
    public class ErrorOutputTo
    {
        public ErrorOutputTo()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

        [JsonIgnore]
        public bool HasErrors => Errors.Any();

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}