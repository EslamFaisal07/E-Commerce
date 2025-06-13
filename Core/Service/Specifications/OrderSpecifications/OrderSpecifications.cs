using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications.OrderSpecifications
{
    internal class OrderSpecifications :BaseSpecifications<Order , Guid>
    {
        public OrderSpecifications(string email ):base(o=>o.UserEmail == email)
        {
            AddInclude(o=>o.DeliveryMethod);
            AddInclude(o=>o.Items);

            AddOrderByDescending(o => o.OrderDate);


        }

        public OrderSpecifications(Guid id ) : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }




    }
}
