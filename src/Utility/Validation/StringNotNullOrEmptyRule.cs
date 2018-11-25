using System;
using System.Collections.Generic;
using System.Text;

namespace Validation
{
    public class StringNotNullOrEmptyRule : BaseRule<string>
    {
        public StringNotNullOrEmptyRule(string model):base(model)
        {
        }


        public override bool Validate()
        {
            var result = !string.IsNullOrEmpty(Model);
            if (!result) Error.SetError(1, "The cannot be null or Empty");
            return result;
        }
    }
}
