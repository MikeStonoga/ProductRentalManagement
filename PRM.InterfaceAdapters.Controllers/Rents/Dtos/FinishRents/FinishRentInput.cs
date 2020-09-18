using System;
using PRM.UseCases.Rents.FinishRents;

namespace PRM.InterfaceAdapters.Controllers.Rents.Dtos.FinishRents
{
    public class FinishRentInput : FinishRentRequirement
    {
        public FinishRentInput()
        {
            
        }
        
        public FinishRentInput(Guid rentId, decimal damageFee, decimal discount) : base(rentId, damageFee, discount)
        {
        }
    }
}