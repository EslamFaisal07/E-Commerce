using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService

    {


        Task<IEnumerable<ProductDTo>> GetAllProductsAsync();


        Task<ProductDTo> GetByIdAsync(int id);



        Task<IEnumerable<TypeDTo>> GetAllTypesAsync();


        Task<IEnumerable<BrandDTo>> GetAllBrandsAsync();





    }
}
