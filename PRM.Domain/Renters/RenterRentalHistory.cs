using System;
using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.Extensions;
using PRM.Domain.Rents;

namespace PRM.Domain.Renters
{
    public sealed class RenterRentalHistory : FullAuditedEntity
    {
        #region Properties
        public Guid RentId { get; private set; }
        public Guid RenterId { get; private set; }

        #endregion

        #region Constructors

        private RenterRentalHistory() { }
        
        public RenterRentalHistory(Guid id, Rent rent, Renter renter) 
            : base(
                id, 
                name: renter.Name + " - " + renter.GovernmentRegistrationDocumentCode + " - " + rent.RentPeriod.StartDate.FormatDate() + " - " + rent.RentPeriod.EndDate.FormatDate(), 
                code: Guid.NewGuid().ToString().Substring(0,7), 
                rent.CreatorId
            )
        {
            RentId = rent.Id;
            RenterId = renter.Id;
        }

        #endregion
    }
}