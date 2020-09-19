using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRM.Domain.BaseCore.ValueObjects
{
    public class Password : ValueObject
    {
        public string Value { get; private set; }

        
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ValidationException("Password is required!");
            if (value.Length < 6) throw new ValidationException("Password min length is 6!");
            
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}