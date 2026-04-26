using AutoMapper;
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
    private readonly Lazy<IFoodItemService> _foodService;
    private readonly Lazy<IBasketService> _BasketService;
    private readonly Lazy<IOrderService> _orderService;

    public ServiceManager(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IJwtTokenService jwtTokenService,
        IConsumerRepository consumerRepo,
        IRestaurantRepository restaurantRepo,
        ICharityRepository charityRepo,
        IDeliveryPartnerRepository deliveryRepo,
        IPaymentRepository paymentRepo,
        IFoodItemRepository foodItemRepository,
        IMapper mapper
        , IBasketRepository basketRepository,
        IOrderService orderService)
    {
        _authService = new(() => new AuthenticationService(userManager, jwtTokenService));
        _restaurantService = new(() => new RestaurantService(unitOfWork, restaurantRepo));
        _consumerService = new(() => new ConsumerService(unitOfWork, consumerRepo));
        _charityService = new(() => new CharityService(unitOfWork, charityRepo));
        _deliveryService = new(() => new DeliveryPartnerService(unitOfWork, deliveryRepo));
        _paymentService = new(() => new PaymentService(unitOfWork, paymentRepo));
        _foodService = new(() => new FoodItemService(foodItemRepository, restaurantRepo, mapper, unitOfWork));
        _BasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        _orderService = new Lazy<IOrderService>(() => new OrderService(mapper, unitOfWork, basketRepository));
    }

    public IAuthenticationService AuthenticationService => _authService.Value;
    public IRestaurantService RestaurantService => _restaurantService.Value;
    public IConsumerService ConsumerService => _consumerService.Value;
    public ICharityService CharityService => _charityService.Value;
    public IDeliveryPartnerService DeliveryPartnerService => _deliveryService.Value;
    public IPaymentService PaymentService => _paymentService.Value;

    public IFoodItemService FoodItemService => _foodService.Value;

    public IBasketService BasketService => _BasketService.Value;

    public IOrderService OrderService => _orderService.Value;
}