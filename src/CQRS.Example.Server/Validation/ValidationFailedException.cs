using System;
using System.Collections.Generic;

namespace CQRS.Example.Server.Validation
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException(ICollection<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public ICollection<ValidationError> ValidationErrors { get; }
    }
}