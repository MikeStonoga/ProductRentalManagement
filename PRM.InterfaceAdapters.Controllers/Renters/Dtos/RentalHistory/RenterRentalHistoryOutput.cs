using System;
using PRM.Domain.Renters;

namespace PRM.InterfaceAdapters.Controllers.Renters.Dtos.RentalHistory
{
    public class RenterRentalHistoryOutput
    {
        public Guid Id { get; }

        public string Code { get; }

        public string Name { get; }

        public DateTime CreationTime { get; }

        public Guid CreatorId { get; }

        public DateTime? LastModificationTime { get; }

        public Guid? LastModifierId { get; }

        public Guid RentId { get; }

        public Guid RenterId { get; }
        
        
        private RenterRentalHistoryOutput() { }
        
        public RenterRentalHistoryOutput(RenterRentalHistory history)
        {
            Id = history.Id;
            Code = history.Code;
            Name = history.Name;
            CreationTime = history.CreationTime;
            CreatorId = history.CreatorId;
            LastModificationTime = history.LastModificationTime;
            LastModifierId = history.LastModifierId;
            RentId = history.RentId;
            RenterId = history.RenterId;
        }
    }
}