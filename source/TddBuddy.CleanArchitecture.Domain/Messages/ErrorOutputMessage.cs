using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TddBuddy.CleanArchitecture.Domain.Messages
{
    [Obsolete("Use ErrorOuput instead. I found this name to be too verbose.")]
    public class ErrorOutputMessage
    {
        public List<string> Errors { get; }

        [JsonIgnore]
        public bool HasErrors => Errors.Any();

        public ErrorOutputMessage()
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