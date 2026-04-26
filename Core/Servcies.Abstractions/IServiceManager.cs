using Servcies.Abstractions;

namespace Services.Abstractions;

public interface IServiceManager
{
    IAuthenticationService AuthenticationService { get; }
    IRestaurantService RestaurantService { get; }
    IConsumerService ConsumerService { get; }
    ICharityService CharityService { get; }
    IDeliveryPartnerService DeliveryPartnerService { get; }
    IPaymentService PaymentService { get; }

    IFoodItemService FoodItemService { get; }

    IBasketService BasketService { get; }

    IOrderService OrderService { get; }
}