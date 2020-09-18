using System;
using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.Extensions;
using PRM.Domain.Rents;

namespace PRM.Domain.Renters
{
    public class RenterRentalHistory : FullAuditedEntity
    {
        #region Properties
        public Guid RentId { get; set; }
        public Guid RenterId { get; set; }

        #endregion

        #region Constructors

        public RenterRentalHistory()
        {
            
        }
        
        public RenterRentalHistory(Rent rent, Renter renter, Guid creatorId)
        {
            Name = renter.Name + " - " + renter.GovernmentRegistrationDocumentCode + " - " + rent.RentPeriod.StartDate.FormatDate() + " - " + rent.RentPeriod.EndDate.FormatDate();
            RentId = rent.Id;
            RenterId = renter.Id;
            CreatorId = creatorId;
        }

        #endregion
    }
}