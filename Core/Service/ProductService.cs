using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork  , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTo>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAllAsync(); // return IEnumrable<productbrand> but need Dto 
            var brandDtos = _mapper.Map<IEnumerable<ProductBrand> , IEnumerable<BrandDTo>>(brands);
            return brandDtos;


        }




        public async Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams)
        {


            var spec = new ProductWithBrandAndTypeSpecification(queryParams);
            var repo = _unitOfWork.GetRepository<Product, int>();

            var products = await repo.GetAllAsync(spec);
            var productDTo = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(products);


            var CountSpec = new ProductCountSpescification(queryParams);

            var productCount = products.Count();
            var TotalCount = await repo.CountAsync(CountSpec);

            return new PaginatedResult<ProductDTo>(queryParams.PageIndex , productCount , TotalCount, productDTo);
        }



        public async Task<IEnumerable<TypeDTo>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var typesDTo = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTo>>(types);
            return typesDTo;



        }




        public async Task<ProductDTo> GetByIdAsync(int id)
        {

            var spec = new ProductWithBrandAndTypeSpecification(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);

         
                var productDTo = _mapper.Map<Product, ProductDTo>(product);
                return productDTo;


        



        }


    }
}
