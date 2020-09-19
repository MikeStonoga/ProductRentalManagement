using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRM.Domain.BaseCore.ValueObjects
{
    public class Phone : ValueObject
    {
        public string RegionCode { get; private set; }
        public string Number { get; private set; }
        
        private Phone(){}
        
        public Phone(string regionCode, string number)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ValidationException("Phone is Required!");
            if (number.Length != 9) throw new ValidationException("Invalid Phone: Number must have 9 digits!");
            
            if (string.IsNullOrWhiteSpace(regionCode)) throw new ValidationException("Region Code is Required!");
            if (regionCode.Length != 3) throw new ValidationException("Invalid Phone: Region Code must have 3 digits!");
            
            RegionCode = regionCode;
            Number = number;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
            yield return RegionCode;
        }
    }
}