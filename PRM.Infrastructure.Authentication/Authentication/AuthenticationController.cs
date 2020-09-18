using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRM.Infrastructure.Authentication.Services;
using PRM.Infrastructure.Authentication.Users;
using PRM.Infrastructure.Persistence.MySQL;

namespace PRM.Infrastructure.Authentication.Authentication
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IReadOnlyRepository<User> _users;

        public AuthenticationController(IReadOnlyRepository<User> users)
        {
            _users = users;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>>Authenticate([FromBody] User input)
        {
            var userRepositoryResponse = await _users.First(user => user.Login == input.Login && user.Password == input.Password );
            if (!userRepositoryResponse.Success) return NotFound(new {message = "Invalid Login / Password"});

            var token = TokenService.GenerateAuthenticationToken(userRepositoryResponse.Response);
            return token;

        }
    }
}