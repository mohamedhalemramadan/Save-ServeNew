using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Servcies.Abstractions;
using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies
{
    public class OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResult> CreateOrUpdateOrderAsync(OrderRequest request, string userEmail)
        {
            //1- Address
            var Address = mapper.Map<shippingDetails>(request.shipToAddress);
            //2- Order Items=> Basket => BasketItems
            var Basket = await basketRepository.GetBasketAsync(request.BasketId)
                ?? throw new Exception(request.BasketId);
            var OrderItems = new List<OrderItem>();
            foreach (var item in Basket.Items)
            {
                var FoodItem = await unitOfWork.GetRepository<FoodItem, int>().GetByIdAsync
                   (item.Id) ?? throw new Exception("error in the item id");
                OrderItems.Add(CreateOrderItem(item, FoodItem));
            }
            var OrderRepo = unitOfWork.GetRepository<Order, Guid>();




            //4- SubTotal
            var SubTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            //Save To DataBase
            var Order = new Order(userEmail, Address, OrderItems, SubTotal);
            await OrderRepo.AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
            // Map And Return
            return mapper.Map<OrderResult>(Order);







        }

        private OrderItem CreateOrderItem(BasketItem item, FoodItem foodItem)
        => new OrderItem(foodItem.Id, foodItem.Name,
            item.quantity, foodItem.Price);





        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var Order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(id)

                ?? throw new Exception("id in not found ");
            return mapper.Map<OrderResult>(Order);
        }
    }
}
