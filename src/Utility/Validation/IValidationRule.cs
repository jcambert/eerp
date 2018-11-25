using System;
using System.Collections.Generic;
using System.Text;

namespace Validation
{


    public interface IValidationRule
    {
        ValidationError Error { get; }
        bool Validate();
    }
    public interface IValidationRule<TModel>:IValidationRule
    {
        

        TModel Model { get; }

        
    }
}
