using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRM.Domain.BaseCore.ValueObjects
{
    public class Login : ValueObject
    {
        public string Value { get; private set; }

        
        public Login(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ValidationException("Login is required!");
            if (value.Length < 3) throw new ValidationException("Login min length is 3");
            
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}