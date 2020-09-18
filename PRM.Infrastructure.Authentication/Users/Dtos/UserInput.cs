using PRM.InterfaceAdapters.Controllers.BaseCore;

namespace PRM.Infrastructure.Authentication.Users.Dtos
{
    public class UserInput : User, IAmManipulationInput<User>
    {
        public string PasswordConfirmation { get; set; }
        public User MapToEntity()
        {
            return new User
            {
                Email = Email,
                Login = Login,
                Phone = Phone,
                Name = Name,
                Password = Password,
                Role = Role,
                PersonImage = PersonImage,
                CreatorId = CreatorId,
                LastModifierId = LastModifierId,
                BirthDate = BirthDate
            };
        }
    }
}