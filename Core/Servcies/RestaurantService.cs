using Domain.Contracts.Domain.Contracts;
using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared.Restaurant;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
        {
            _unitOfWork = unitOfWork;
            _restaurantRepository = restaurantRepository;
        }

        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RestaurantDto> GetByIdAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
                throw new Exception("Restaurant not found");

            return MapToDto(restaurant);
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return restaurants.Select(r => MapToDto(r));
        }

        public async Task<RestaurantDto> GetByUserIdAsync(string userId)
        {
            var restaurant = await _restaurantRepository.GetByUserIdAsync(userId);
            if (restaurant == null)
                throw new Exception("Restaurant not found for this user");

            return MapToDto(restaurant);
        }

        public async Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto, string userId)
        {
            // Check if user already has a restaurant
            var existing = await _restaurantRepository.GetByUserIdAsync(userId);
            if (existing != null)
                throw new Exception("User already has a restaurant");

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
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
                throw new Exception("Restaurant not found");

            restaurant.OpeningHours = dto.OpeningHours ?? restaurant.OpeningHours;
            restaurant.ClosingHours = dto.ClosingHours ?? restaurant.ClosingHours;
            restaurant.Type = dto.Type ?? restaurant.Type;
            restaurant.Rating = dto.Rating;

            _restaurantRepository.Update(restaurant);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(restaurant);
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
                throw new Exception("Restaurant not found");

            _restaurantRepository.Delete(restaurant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<RestaurantDto>> GetByTypeAsync(string type)
        {
            var restaurants = await _restaurantRepository.GetByTypeAsync(type);
            return restaurants.Select(r => MapToDto(r));
        }

        // Helper method
        private RestaurantDto MapToDto(Restaurant restaurant)
        {
            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.User?.DisplayName ?? "N/A",
                OpeningHours = restaurant.OpeningHours,
                ClosingHours = restaurant.ClosingHours,
                Type = restaurant.Type,
                Rating = restaurant.Rating
            };
        }
    }
}
