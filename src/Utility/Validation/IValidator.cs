using System;
using System.Collections.Generic;
using System.Text;

namespace Validation
{
    public interface IValidator
    {
        IValidator AddRule<TRule,TModel>(TModel model) where TRule:IValidationRule<TModel>;
        ValidationResult Validate();
        IObservable<ValidationError> OnError { get; }
        IObservable<ValidationResult> OnValidate { get; }
        bool HasError { get; }
        void Clear();
    }
}
