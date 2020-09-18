using PRM.Domain.Renters;

namespace PRM.InterfaceAdapters.Controllers.Renters.Dtos
{
    public class RenterOutput : Renter
    {

        public RenterOutput()
        {
            
        }
        
        public RenterOutput(Renter renter)
        {
            Id = renter.Id;
            Code = renter.Code;
            Name = renter.Name;
            CreationTime = renter.CreationTime;
            CreatorId = renter.CreatorId;
            DeleterId = renter.DeleterId;
            DeletionTime = renter.DeletionTime;
            IsDeleted = renter.IsDeleted;
            LastModificationTime = renter.LastModificationTime;
            LastModifierId = renter.LastModifierId;
            PersonImage = renter.PersonImage;
            Email = renter.Email;
            Phone = renter.Phone;
            BirthDate = renter.BirthDate;
            GovernmentRegistrationDocumentCode = renter.GovernmentRegistrationDocumentCode;
        }
    }
}