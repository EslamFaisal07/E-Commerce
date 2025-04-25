using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork , IMapper mapper , IBasketRepository basketRepository) : IServiceManager
    {

        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));

        public IProductService productService => _LazyproductService.Value;


        private readonly Lazy<IBasketServices> _LazybasketServices = new Lazy<IBasketServices>(() => new BasketService(basketRepository, mapper));



        public IBasketServices BasketService => _LazybasketServices.Value;
    }
}
