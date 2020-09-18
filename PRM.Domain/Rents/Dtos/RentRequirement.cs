using System;
using System.Collections.Generic;

namespace PRM.Domain.Rents.Dtos
{
    public class RentRequirement
    {
        #region Properties
        public Guid RenterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;  }
        public List<Guid> ProductsIds { get; set; }

        #endregion
    }
}