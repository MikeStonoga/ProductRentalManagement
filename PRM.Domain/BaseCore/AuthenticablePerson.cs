using System;
using PRM.Domain.BaseCore.ValueObjects;

namespace PRM.Domain.BaseCore
{ 
    public interface IAuthenticablePerson : IPerson
    {
        Login Login { get; }
        Password Password { get; }
        void SetLogin(Login newLogin);
        void SetPassword(Password newPassword);
        bool Authenticate(Login inputLogin, Password inputPassword);
    }
    
    public abstract class AuthenticablePerson : Person, IAuthenticablePerson
    {
        public Login Login { get; private set; }
        public Password Password { get; private set; }
        
        protected AuthenticablePerson() {}

        public AuthenticablePerson(Guid id, string name, string code, Guid creatorId, GovernmentRegistrationDocumentCode governmentRegistrationDocumentCode, Email email, Phone phone, BirthDate birthDate, byte[] personImage, Login login, Password password) 
            : base(id, name, code, creatorId, governmentRegistrationDocumentCode, email, phone, birthDate, personImage)
        {
            Login = login;
            Password = password;
        }

        public void SetLogin(Login newLogin) => Login = newLogin;
        public void SetPassword(Password newPassword) => Password = newPassword;


        public bool Authenticate(Login inputLogin, Password inputPassword)
        {
            var isSameLogin = Login.Value == inputLogin.Value;
            var isSamePassword = Password.Value == inputPassword.Value;
            return isSameLogin && isSamePassword;
        }

    }
}