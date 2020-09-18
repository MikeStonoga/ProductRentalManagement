using System;

namespace PRM.UseCases.Rents.FinishRents
{
    public class FinishRentRequirement
    {
        public Guid RentId { get; set; }
        public decimal DamageFee { get; set; }
        public decimal Discount { get; set;  }

        public FinishRentRequirement()
        {
        }
        
        public FinishRentRequirement(Guid rentId, decimal damageFee, decimal discount)
        {
            RentId = rentId;
            DamageFee = damageFee;
            Discount = discount;
        }
    }
}