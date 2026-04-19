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
}