using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthanticationService(UserManager<ApplicationUser> _userManager  , IConfiguration _configuration) : IAuthanticationService
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
                    Token = await CreateTokenAsync(user)
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
                    Token =  await CreateTokenAsync(user)
                };
            }
            else
            {
                // if not throw badrequest exception
                var errors = result.Errors.Select(e=>e.Description).ToList();
                throw new BadRequestExcception(errors);
                
            }








        }






        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {

            var claims = new List<Claim>()
            {
                new (ClaimTypes.Email , user.Email!),
                new (ClaimTypes.Name , user.UserName!),
                new (ClaimTypes.NameIdentifier , user.Id!),
               
            };
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = _configuration.GetSection("JWTOptions")["SecretKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                
                issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:creds


                );



            return new JwtSecurityTokenHandler().WriteToken(token);

        }









    }
}
