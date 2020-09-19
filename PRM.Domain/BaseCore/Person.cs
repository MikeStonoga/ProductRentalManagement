using System;
using PRM.Domain.BaseCore.ValueObjects;

namespace PRM.Domain.BaseCore
{
    public interface IPerson : IFullAuditedEntity
    {
        GovernmentRegistrationDocumentCode GovernmentRegistrationDocumentCode { get; }
        byte[] PersonImage { get; }
        Email Email { get; }
        Phone Phone { get; }
        BirthDate BirthDate { get; }
        void UpdatePhone(Phone newPhone);
        void UpdateEmail(Email newEmail);
        void UpdatePersonImage(byte[] newPersonImage);
        void UpdateBirthDate(BirthDate newBirthDate);
    }

    public abstract class Person : FullAuditedEntity, IPerson
    {
        public GovernmentRegistrationDocumentCode GovernmentRegistrationDocumentCode { get; private set; }
        public byte[] PersonImage { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }  
        
        public BirthDate BirthDate { get; private set; }
        
        protected Person()  {}

        public Person(Guid id, string name, string code, Guid creatorId, GovernmentRegistrationDocumentCode governmentRegistrationDocumentCode, Email email, Phone phone, BirthDate birthDate, byte[] personImage) : base(id, name, code, creatorId)
        {
            GovernmentRegistrationDocumentCode = governmentRegistrationDocumentCode;
            Email = email;
            Phone = phone;
            PersonImage = personImage;
            BirthDate = birthDate;
        }

        public void UpdatePhone(Phone newPhone)
        {
            if (Phone.Number != newPhone.Number)
            {
                Phone = newPhone;
            }
        }

        public void UpdateEmail(Email newEmail)
        {
            if (Email.Address != newEmail.Address)
            {
                Email = newEmail;
            }
        }

        public void UpdatePersonImage(byte[] newPersonImage) => PersonImage = newPersonImage;
        public void UpdateBirthDate(BirthDate newBirthDate) => BirthDate = newBirthDate;
    }
}