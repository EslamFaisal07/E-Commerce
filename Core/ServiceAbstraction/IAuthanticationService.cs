using Shared.DataTransferObjects.IdentityDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthanticationService
    {


        // login takes email , password ==> login dto ---- returns token , email , displayname ==> user dto

        Task<UserDTo> LoginAsync(LoginDTo loginDTo);






        // register takes email , password , displayname , username ==> RegisterDTo ---- returns token , email ,displayname  ==> user dto

        Task<UserDTo> RegisterAsync(RegisterDTo registerDTo);

        // check email takes email ==> return boolean


        Task<bool> CheckEmailAsync(string email);

        // get current user address
        // take email then return address

        Task<AddressDTo> GetCurrentUserAddressAsync(string email);

        //get current user 
        // take email then return token , email and displayname

        Task<UserDTo> GetCurrentUserAsync(string email);

        //update current user address
        // take updated address and email then return address after update 

        Task<AddressDTo> UpdateCurrentUserAddressAsync(string email , AddressDTo addressDTo);





    }
}
