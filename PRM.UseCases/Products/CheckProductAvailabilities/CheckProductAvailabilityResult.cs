using System;

namespace PRM.UseCases.Products.CheckAvailabilities
{
    public class CheckProductAvailabilityResult
    {
        public bool IsAvailable { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public CheckProductAvailabilityResult(bool isAvailable)
        {
            IsAvailable = isAvailable;
            AvailabilityDate = DateTime.Now;
        }
        
        public CheckProductAvailabilityResult()
        {
        }

        public CheckProductAvailabilityResult(DateTime availabilityDate)
        {
            IsAvailable = false;
            AvailabilityDate = availabilityDate;
        }
    }
}