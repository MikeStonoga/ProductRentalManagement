using PRM.UseCases.Products.CheckAvailabilities;

namespace PRM.InterfaceAdapters.Controllers.Products.Dtos.CheckProductAvailabilities
{
    public class CheckProductAvailabilityOutput : CheckProductAvailabilityResult
    {
        public CheckProductAvailabilityOutput()
        {
            
        }

        public CheckProductAvailabilityOutput(CheckProductAvailabilityResult result)
        {
            IsAvailable = result.IsAvailable;
            AvailabilityDate = result.AvailabilityDate;
        }
    }
}