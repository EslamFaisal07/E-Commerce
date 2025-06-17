using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> ProductFactory,
        Func<IBasketServices> BasketFactory ,
        Func<IAuthanticationService> AuthanticationFactory,
        Func<IOrderService> OrderFactory
        ) : IServiceManager
    {
        public IProductService productService => ProductFactory.Invoke();

        public IBasketServices BasketService => BasketFactory.Invoke();

        public IAuthanticationService AuthanticationService => AuthanticationFactory.Invoke();

        public IOrderService OrderService => OrderFactory.Invoke();










    }
}
