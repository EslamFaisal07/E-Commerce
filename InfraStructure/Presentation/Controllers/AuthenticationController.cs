using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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



        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManager.AuthanticationService.CheckEmailAsync(email);

            return Ok(result);
        }




        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTo>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthanticationService.GetCurrentUserAsync(email!);
            return Ok(AppUser);
        }


        [Authorize]
        [HttpGet("Address")]

        public async Task<ActionResult<AddressDTo>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthanticationService.GetCurrentUserAddressAsync(email!);
            return Ok(address);


        }



        [Authorize]
        [HttpPut]

        public async Task<ActionResult<AddressDTo>> UpdateCurrentUserAddress(AddressDTo addressDTo)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await _serviceManager.AuthanticationService.UpdateCurrentUserAddressAsync(email, addressDTo);
            return Ok(updatedAddress);

        }




    }
}
