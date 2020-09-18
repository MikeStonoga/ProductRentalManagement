using PRM.Domain.BaseCore;

namespace PRM.Domain.Renters
{
    public class Renter : Person
    {
        public string GovernmentRegistrationDocumentCode { get; set; }
    }
}