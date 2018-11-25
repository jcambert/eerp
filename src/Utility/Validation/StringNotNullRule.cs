using System;
using System.Collections.Generic;
using System.Text;

namespace Validation
{
    public class StringNotNullRule : BaseRule<string>
    {
        public StringNotNullRule(string model) : base(model)
        {
        }
        

        public override bool Validate()
        {
            var result =(Model!=null);
            if (!result)
                Error.SetError(1,"The cannot be null");
            return result;
        }
    }
}
