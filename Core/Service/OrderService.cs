using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications.OrderSpecifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTos;
using Shared.DataTransferObjects.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper , IBasketRepository _basketRepository , IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTo> CreateOrder(OrderDTo orderDTo, string Email)
        {
           

            //map address to order address

            var orderAddress = _mapper.Map<AddressDTo, OrderAddress>(orderDTo.Address);



            // get basket 
            var basket = await _basketRepository.GetBasketAsync(orderDTo.BasketId) ?? throw new BasketNotFoundException(orderDTo.BasketId);

            //create order item list
            List<OrderItem> orderItems = new List<OrderItem>();
            var productRepo =  _unitOfWork.GetRepository<Product , int>();
            foreach (var item in basket.Items)
            {

                var product = await productRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                OrderItem orderItem = CreateOrderItem(item, product);
                orderItems.Add(orderItem);
            }
            //get delivery method

            var deliveryMethodRepo = _unitOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryMethodRepo.GetByIdAsync(orderDTo.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(orderDTo.DeliveryMethodId);




            //calculate subtotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //create order
            var order = new Order(Email, orderAddress, deliveryMethod, subTotal, orderItems);

          await  _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order, OrderToReturnDTo>(order);



        }


        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrder()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl,


                },
                Price = product.Price,
                Quantity = item.Quantity

            };
        }
        public async Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTo>>(DeliveryMethods);
        }




        public async Task<IEnumerable<OrderToReturnDTo>> GetAllOrserAsync(string Email)
        {
            var spec = new OrderSpecifications(Email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTo>>(orders);

        }

        public async Task<OrderToReturnDTo> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecifications(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);

            return _mapper.Map<Order, OrderToReturnDTo>(order);


        }
    }
}
