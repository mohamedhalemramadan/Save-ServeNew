using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared;
using Shared.Restaurant;

namespace Servcies;

public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
    {
        _unitOfWork = unitOfWork;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<RestaurantDto?> GetByIdAsync(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        return restaurant is null ? null : MapToDto(restaurant);
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return restaurants.Select(MapToDto);
    }

    public async Task<RestaurantDto?> GetByUserIdAsync(string userId)
    {
        var restaurant = await _restaurantRepository.GetByUserIdAsync(userId);
        return restaurant is null ? null : MapToDto(restaurant);
    }

    public async Task<IEnumerable<RestaurantDto>> GetByTypeAsync(string type)
    {
        var restaurants = await _restaurantRepository.GetByTypeAsync(type);
        return restaurants.Select(MapToDto);
    }

    public async Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto, string userId)
    {
        var existing = await _restaurantRepository.GetByUserIdAsync(userId);
        if (existing is not null)
            throw new InvalidOperationException("User already has a restaurant registered.");

        var restaurant = new Restaurant
        {
            UserId = userId, 
            OpeningHours = dto.OpeningHours,
            ClosingHours = dto.ClosingHours,
            Type = dto.Type,
            Rating = dto.Rating
        };

        await _restaurantRepository.AddAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(restaurant);
    }
   

    public async Task<RestaurantDto> UpdateAsync(int id, UpdateRestaurantDto dto)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Restaurant not found");

        restaurant.OpeningHours = dto.OpeningHours ?? restaurant.OpeningHours;
        restaurant.ClosingHours = dto.ClosingHours ?? restaurant.ClosingHours;
        restaurant.Type = dto.Type ?? restaurant.Type;
        if (dto.Rating != default(decimal)) 
            restaurant.Rating = (decimal)dto.Rating;

        _restaurantRepository.Update(restaurant);
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(restaurant);
    }

    public async Task DeleteAsync(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Restaurant not found");

        _restaurantRepository.Delete(restaurant);
        await _unitOfWork.SaveChangesAsync();
    }

    private static RestaurantDto MapToDto(Restaurant r) => new()
    {
        Id = r.Id,
        //Name = r.User?.DisplayName ?? "N/A",
        OpeningHours = r.OpeningHours,
        ClosingHours = r.ClosingHours,
        Type = r.Type,
        Rating = r.Rating
    };
}