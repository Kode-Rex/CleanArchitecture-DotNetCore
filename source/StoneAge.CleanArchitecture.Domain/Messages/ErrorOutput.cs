using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace StoneAge.CleanArchitecture.Domain.Messages
{
    public class ErrorOutput
    {
        public List<string> Errors { get; }

        [JsonIgnore]
        public bool HasErrors => Errors != null && Errors.Any();

        public ErrorOutput()
        {
            Errors = new List<string>();
        }

        public ErrorOutput(string error)
        {
            Errors = new List<string>();
            if (error != null)
            {
                Errors = new List<string>{error};
            }
        }

        public ErrorOutput(List<string> errors)
        {
            if (errors != null)
            {
                Errors = errors;
            }
            else
            {
                Errors = new List<string>();
            }
        }
        
        public void AddError(string error)
        {
            if (error != null)
            {
                Errors.Add(error);
            }
        }

        public void AddErrors(List<string> errors)
        {
            Errors.AddRange(errors);
        }
    }
}