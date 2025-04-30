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






    }
}
