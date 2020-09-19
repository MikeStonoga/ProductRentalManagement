using System;
using PRM.Domain.BaseCore;
using PRM.Domain.BaseCore.ValueObjects;

namespace PRM.Domain.Renters
{
    public sealed class Renter : Person
    {
        public Renter(Guid id, string name, string code, Guid creatorId, GovernmentRegistrationDocumentCode governmentRegistrationDocumentCode, Email email, Phone phone, BirthDate birthDate, byte[] personImage) 
            : base(id, name, code, creatorId, governmentRegistrationDocumentCode, email, phone, birthDate, personImage)
        {
        }
    }
}