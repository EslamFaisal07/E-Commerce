using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork , IMapper mapper , IBasketRepository basketRepository , UserManager<ApplicationUser> userManager) : IServiceManager
    {

        private readonly Lazy<IProductService> _LazyproductService = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        private readonly Lazy<IBasketServices> _LazybasketServices = new Lazy<IBasketServices>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthanticationService> _LazyAuthanticationService = new Lazy<IAuthanticationService>(() => new AuthanticationService(userManager));

        public IProductService productService => _LazyproductService.Value;





        public IBasketServices BasketService => _LazybasketServices.Value;

        public IAuthanticationService AuthanticationService => _LazyAuthanticationService.Value;
    }
}
