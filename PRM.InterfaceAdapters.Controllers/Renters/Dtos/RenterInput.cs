using PRM.Domain.Renters;
using PRM.InterfaceAdapters.Controllers.BaseCore;

namespace PRM.InterfaceAdapters.Controllers.Renters.Dtos
{
    public class RenterInput : Renter, IAmManipulationInput<Renter>
    {
        public Renter MapToEntity()
        {
            return new Renter
            {
                Id = Id,
                Code = GovernmentRegistrationDocumentCode,
                Name = Name,
                CreationTime = CreationTime,
                CreatorId = CreatorId,
                DeleterId = DeleterId,
                DeletionTime = DeletionTime,
                IsDeleted = IsDeleted,
                LastModificationTime = LastModificationTime,
                LastModifierId = LastModifierId,
                PersonImage = PersonImage,
                Email = Email,
                Phone = Phone,
                BirthDate = BirthDate,
                GovernmentRegistrationDocumentCode = GovernmentRegistrationDocumentCode
            };
        }
    }
}