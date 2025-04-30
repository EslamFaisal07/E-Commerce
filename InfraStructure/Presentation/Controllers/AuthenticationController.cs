using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTo>> Login(LoginDTo loginDTo)
        {
            var user = await _serviceManager.AuthanticationService.LoginAsync(loginDTo);
            return Ok(user);
        }


        [HttpPost("Register")]

        public async Task<ActionResult<UserDTo>> Register(RegisterDTo registerDTo)
        {
            var user = await _serviceManager.AuthanticationService.RegisterAsync(registerDTo);
            return Ok(user);
        }






    }
}
