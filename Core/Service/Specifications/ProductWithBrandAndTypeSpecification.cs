using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product , int>
    {



        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams)
            :base(p=>(!queryParams.BrandId.HasValue ||p.BrandId== queryParams.BrandId) &&
            (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.SearchValue ) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);


            switch(queryParams.SortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                    case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    
                    break;
            }
            ApplyPagination(queryParams.pageSize , queryParams.PageIndex);

        }


        public ProductWithBrandAndTypeSpecification(int id) :base( p=>p.Id ==id)
        {

            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

        }










    }
}
