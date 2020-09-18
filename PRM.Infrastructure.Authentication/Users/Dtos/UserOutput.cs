namespace PRM.Infrastructure.Authentication.Users.Dtos
{
    public class UserOutput : User
    {
        public UserOutput()
        {
            
        }
        
        public UserOutput(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Code = user.Code;
            Email = user.Email;
            Phone = user.Phone;
            Login = user.Login;
            Password = user.Password;
            Role = user.Role;
            PersonImage = user.PersonImage;
            CreationTime = user.CreationTime;
            CreatorId = user.CreatorId;
            DeletionTime = user.DeletionTime;
            DeleterId = user.DeleterId;
            LastModificationTime = LastModificationTime;
            LastModifierId = LastModifierId;
        }
    }
}