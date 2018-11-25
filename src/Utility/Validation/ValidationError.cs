using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace Validation
{
   public  class ValidationError
    {
        ISubject<ValidationError> _errorSubject = new Subject<ValidationError>();
        public ValidationError()
        {
        }

        public void SetError(int code,string message)
        {
            Code = code;
            Message = message;
            if (code != 0 && !string.IsNullOrEmpty(Message))
            {
                _errorSubject.OnNext(this);
                HasError = true;
            }
            else
                HasError = false;

        }
        public string Message { get; private set; } = "";

        public int Code { get; private set; } = 0;

    

        public IObservable<ValidationError> OnError => _errorSubject;

        public bool HasError { get; private set; }
    }
}
