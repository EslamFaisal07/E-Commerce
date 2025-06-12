using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public IProductService productService { get;  }
        public IBasketServices BasketService { get; }
        public IAuthanticationService AuthanticationService { get; }

    }
}
