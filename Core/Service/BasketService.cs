using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository basketRepository , IMapper mapper) : IBasketServices
    {


        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = mapper.Map< BasketDto,CustomerBasket>(basket);

          var IsCreatedOrUpdated = await basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if (IsCreatedOrUpdated is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Cann't Update or create Basket Now Try Again Later");
            }
        }



        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return  await  basketRepository.DeleteBasketAsync(Key);
        }





        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var basket = await basketRepository.GetBasketAsync(Key);
            if (basket is null)
                throw new BasketNotFoundException(Key);


            var basketDto = mapper.Map<BasketDto>(basket);
            return basketDto;
        }










    }
}
