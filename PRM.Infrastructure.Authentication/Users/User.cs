using PRM.Domain.BaseCore;
using PRM.Infrastructure.Authentication.Users.Enums;

namespace PRM.Infrastructure.Authentication.Users
{
    public class User : AuthenticablePerson
    {
        public UserRoles Role { get; set; }
    }
}