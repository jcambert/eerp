using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;

namespace Validation
{
    public class Validator: IValidator 
    {
        List<ValidationError> _errors = new List<ValidationError>();
        ISubject<ValidationError> _errorSubject = new Subject<ValidationError>();
        ISubject<ValidationResult> _validSubject = new Subject<ValidationResult>();

        
        public IValidator AddRule<TRule, TModel>(TModel model) where TRule : IValidationRule<TModel>
        {
            TRule rule =(TRule) Activator.CreateInstance(typeof(TRule),model);
           
            rule.Error.OnError.Subscribe(validationError => { HasError = true;_errors.Add(validationError);  _errorSubject.OnNext(validationError); });
            Rules.Add(rule);

            return this;
        }

        public ValidationResult Validate()
        {
            HasError = false;
            _errors.Clear();

            Rules.ForEach(rule =>
            {
                rule.Validate();
            });

            var result= new ValidationResult(_errors);
            if(!HasError)
                _validSubject.OnNext(result);
            return result;
        }

        public void Clear()
        {
            HasError = false;
            Rules.Clear();
            _errors.Clear();
        }

        public List<IValidationRule> Rules { get; } = new List<IValidationRule>();

        public IObservable<ValidationError> OnError => _errorSubject;

        public IObservable<ValidationResult> OnValidate => _validSubject;

        public bool HasError { get; private set; }
    }

    
}
