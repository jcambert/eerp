using System;
using System.Collections.Generic;
using System.Text;

namespace Validation
{
    public abstract class BaseRule<TModel> : IValidationRule<TModel>
    {
        public BaseRule(TModel model)
        {
            this.Model = model;
            
        }

        public ValidationError Error { get; protected set; } = new ValidationError();

        public TModel Model { get;}

        public abstract bool Validate();
    }
}
