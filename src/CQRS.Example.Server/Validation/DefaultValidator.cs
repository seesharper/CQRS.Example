using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CQRS.Example.Server.Validation
{
    public class DefaultValidator<T> : IValidator<T>
    {
        public void Validate(T value)
        {
            var result = new List<ValidationResult>();
            if (!Validator.TryValidateObject(value, new ValidationContext(value), result, true))
            {
                var errors = result.Select(r => new ValidationError(r.MemberNames.First(), r.ErrorMessage));
                throw new ValidationFailedException(errors.ToList());
            }
        }
    }
}