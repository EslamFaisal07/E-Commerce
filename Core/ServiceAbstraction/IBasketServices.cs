using Shared.DataTransferObjects.BasketModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketServices
    {

        Task<BasketDto> GetBasketAsync(string Key);



        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);


        Task<bool> DeleteBasketAsync(string Key);








    }
}
