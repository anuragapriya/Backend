using Microsoft.AspNetCore.Mvc;
using WGL.Auth.Application.CQRS.Account.Commands.CreateUser;
using WGL.Auth.Application.CQRS.Account.Queries.GenerateToken;
using WGL.Auth.Application.CQRS.Account.Queries.GetUserLoginToken;
using WGL.Auth.Controllers.BaseController;
using WGL.Auth.Domain.Entities;

namespace WGL.Auth.Controllers.Account
{
    public class AccountController : BaseApiController
    {
        [HttpGet("GenerateToken")]
        public async Task<IActionResult> Get(/*string AppId, string SecretKey*/)
        {
            return Ok(await Mediator.Send(new GenerateTokenQuery() /*{ AppId= AppId, SecretKey = SecretKey }*/));
        }

        //Session Login Token
        [HttpGet("GetUserLoginToken")]
        public async Task<IActionResult> GetUserLoginToken(string UserName, string Password)
        {
            return Ok(await Mediator.Send(new GenerateUserLoginTokenQuery() { UserName = UserName, Password = Password }));
        }

        // POST api/
        [HttpPost("Authenticate")]
        public void AuthenticateAsync([FromBody] string value)
        {

        }
        // POST api/
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(CreateUserCommand userCommand)
        {
            var origin = Request.Headers.Origin;
            return Ok(await Mediator.Send(userCommand));
        }
        [HttpPost("reset-password")]
        public void ResetPasswordAsync([FromBody] string value)
        {

        }
        [HttpPost("forgot-password")]
        public void forgotPasswordAsync([FromBody] string value)
        {
            
        }
        // POST api/
        [HttpPut]
        public void put([FromBody] string value)
        {

        }
        // POST api/
        [HttpPatch]
        public void Patch([FromBody] string value)
        {

        }
        // POST api/
        [HttpDelete]
        public void Delete([FromBody] string value)
        {

        }

    }
}
