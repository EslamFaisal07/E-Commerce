using Shared.DataTransferObjects.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {

        Task<OrderToReturnDTo> CreateOrder(OrderDTo orderDTo, string Email);

        Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodsAsync();

        Task<IEnumerable<OrderToReturnDTo>> GetAllOrserAsync(string Email);
        Task<OrderToReturnDTo> GetOrderByIdAsync(Guid id);

    }
}
