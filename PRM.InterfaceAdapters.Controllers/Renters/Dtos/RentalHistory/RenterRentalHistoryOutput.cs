using PRM.Domain.Renters;

namespace PRM.InterfaceAdapters.Controllers.Renters.Dtos.RentalHistory
{
    public class RenterRentalHistoryOutput : RenterRentalHistory
    {
        public RenterRentalHistoryOutput()
        {
            
        }
        
        public RenterRentalHistoryOutput(RenterRentalHistory history)
        {
            Id = history.Id;
            Code = history.Code;
            Name = history.Name;
            CreationTime = history.CreationTime;
            CreatorId = history.CreatorId;
            DeleterId = history.DeleterId;
            DeletionTime = history.DeletionTime;
            IsDeleted = history.IsDeleted;
            LastModificationTime = history.LastModificationTime;
            LastModifierId = history.LastModifierId;
            RentId = history.RentId;
            RenterId = history.RenterId;
        }
    }
}