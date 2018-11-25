using System.Collections.Generic;

namespace Validation
{
    public class ValidationResult
    {
        private List<ValidationError> _errors;

        internal ValidationResult(List<ValidationError> errors)
        {
            _errors = errors;
        }
        public bool Valid => _errors.Count == 0;

        public List<ValidationError> Errors => _errors;
    }
}