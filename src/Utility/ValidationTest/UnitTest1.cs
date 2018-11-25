using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validation;
using System;
using System.Diagnostics;

namespace ValidationTest
{

    public class Toto : IObserver<ValidationError>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(ValidationError value)
        {
           
        }
    }
    [TestClass]
    public class UnitTest1
    {
        public void OnError(ValidationError error)
        {
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            string s0 = "",s1="1234",s3=null;
            

            IValidator validator = new Validator();
            validator.OnError.Subscribe(error=>
            {
                Debug.WriteLine("An error occur:" + error.Message);
               
            });
            validator.OnValidate.Subscribe(valid =>
            {
                Assert.IsTrue(valid.Valid);
            });
            validator
                .AddRule<StringNotNullRule,string>(s0)
                .AddRule<StringNotNullOrEmptyRule,string>(s0);
            validator.Validate();
            Assert.IsTrue(validator.HasError);
           


            validator.Clear();
            validator.AddRule<StringNotNullRule, string>(s3);
            validator.Validate();
        }
    }
}
