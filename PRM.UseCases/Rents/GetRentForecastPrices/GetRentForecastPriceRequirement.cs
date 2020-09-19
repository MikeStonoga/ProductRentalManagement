using System;
using System.Collections.Generic;

namespace PRM.UseCases.Rents.GetRentForecastPrices
{
    public class GetRentForecastPriceRequirement
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;  }
        public List<Guid> ProductsIds { get; set; }
        public Guid CreatorId { get; set; }
    }
}