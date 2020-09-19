using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRM.Domain.BaseCore.ValueObjects
{
    public class GovernmentRegistrationDocumentCode : ValueObject
    {
        public string Number { get; private set; }

        
        public GovernmentRegistrationDocumentCode(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ValidationException("Government registration document code number is required!");
            if (number.Length != 11 && number.Length != 14) throw new ValidationException("Invalid government registration document code number length!");
            Number = number;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
        }
    }
}