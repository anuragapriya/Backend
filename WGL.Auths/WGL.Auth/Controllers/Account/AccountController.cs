using Microsoft.AspNetCore.Mvc;
using WGL.Auth.Application.CQRS.Account.Queries.GenerateToken;
using WGL.Auth.Application.CQRS.Account.Queries.GetUserLoginToken;
using WGL.Auth.Controllers.BaseController;

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
        [HttpPost]
        public void Post([FromBody] string value)
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
