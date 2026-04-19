using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Servcies.Abstractions;
using Services;
using Services.Abstractions;

namespace Servcies;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authService;
    private readonly Lazy<IRestaurantService> _restaurantService;
    private readonly Lazy<IConsumerService> _consumerService;
    private readonly Lazy<ICharityService> _charityService;
    private readonly Lazy<IDeliveryPartnerService> _deliveryService;
    private readonly Lazy<IPaymentService> _paymentService;

    public ServiceManager(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IJwtTokenService jwtTokenService,
        IConsumerRepository consumerRepo,
        IRestaurantRepository restaurantRepo,
        ICharityRepository charityRepo,
        IDeliveryPartnerRepository deliveryRepo,
        IPaymentRepository paymentRepo)
    {
        _authService = new(() => new AuthenticationService(userManager, jwtTokenService));
        _restaurantService = new(() => new RestaurantService(unitOfWork, restaurantRepo));
        _consumerService = new(() => new ConsumerService(unitOfWork, consumerRepo));
        _charityService = new(() => new CharityService(unitOfWork, charityRepo));
        _deliveryService = new(() => new DeliveryPartnerService(unitOfWork, deliveryRepo));
        _paymentService = new(() => new PaymentService(unitOfWork, paymentRepo));
    }

    public IAuthenticationService AuthenticationService => _authService.Value;
    public IRestaurantService RestaurantService => _restaurantService.Value;
    public IConsumerService ConsumerService => _consumerService.Value;
    public ICharityService CharityService => _charityService.Value;
    public IDeliveryPartnerService DeliveryPartnerService => _deliveryService.Value;
    public IPaymentService PaymentService => _paymentService.Value;
}