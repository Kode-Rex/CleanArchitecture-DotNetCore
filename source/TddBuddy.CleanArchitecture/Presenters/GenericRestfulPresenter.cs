using System;
using Microsoft.AspNetCore.Mvc;

namespace TddBuddy.CleanArchitecture.Presenters
{
    public class GenericRestfulPresenter
    {
        private IActionResult _response;

        public void RespondWith(IActionResult response)
        {
            _response = response;
        }

        public IActionResult Render()
        {
            if (IsAnyResponsesSpecified())
            {
                return _response;
            }

            throw new InvalidOperationException("No response specified.");
        }

        private bool IsAnyResponsesSpecified()
        {
            return _response != null;
        }
    }
}