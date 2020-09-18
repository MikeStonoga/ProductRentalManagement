using System.Collections.Generic;
using PRM.Domain.BaseCore.ValueObjects;
using PRM.Domain.Products;
using PRM.Domain.Renters;

namespace PRM.UseCases.Rents.Validations
{
    public class RentRequirementValidationResult
    {
        public DateRange RentPeriod { get; set; }
        public Renter Renter { get; set; }
        public List<Product> Products { get; set; }
    }
}