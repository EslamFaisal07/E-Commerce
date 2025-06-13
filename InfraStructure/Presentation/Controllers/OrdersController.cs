using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        // create order 

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTo>> CreateOrder(OrderDTo orderDTo)
        {

            var order = await _serviceManager.OrderService.CreateOrder(orderDTo, GetEmailFromToken());
            return Ok(order);

        }

        [AllowAnonymous]
        //get delivery methods 
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTo>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTo>>> GetAllOrders()
        {
           var orders = await _serviceManager.OrderService.GetAllOrserAsync(GetEmailFromToken());
            return Ok(orders);
        }


        
        [ HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTo>> GetOrderById(Guid id)
        {
            var order=  await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }



    }
}
