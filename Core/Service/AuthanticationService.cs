using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthanticationService(UserManager<ApplicationUser> _userManager) : IAuthanticationService
    {
        public async Task<UserDTo> LoginAsync(LoginDTo loginDTo)
        {

            var user = await _userManager.FindByEmailAsync(loginDTo.Email);
            if (user == null)
            {
                throw new UserNotFoundException(loginDTo.Email);            
            }

            var IsPasswordValid = await   _userManager.CheckPasswordAsync(user , loginDTo.Password);

            if (IsPasswordValid)
            {
                return new UserDTo()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = CreateTokenAsync(user)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }



        }



        public async Task<UserDTo> RegisterAsync(RegisterDTo registerDTo)
        {
            // mapping from register dto to application user
            var user = new ApplicationUser()
            {
                DisplayName = registerDTo.DisplayName,
                Email = registerDTo.Email,
                PhoneNumber = registerDTo.PhoneNumber,
                UserName = registerDTo.UserName,
            };

            // create user 

            var result = await _userManager.CreateAsync(user , registerDTo.Password);

            if (result.Succeeded )
            {
                // if created  return usert dto 
                return new UserDTo()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = CreateTokenAsync(user)
                };
            }
            else
            {
                // if not throw badrequest exception
                var errors = result.Errors.Select(e=>e.Description).ToList();
                throw new BadRequestExcception(errors);
                
            }








        }

        private static string CreateTokenAsync(ApplicationUser user)
        {
            return "---";
        }









    }
}
