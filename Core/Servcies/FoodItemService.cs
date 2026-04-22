using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared;
using Shared.Fooditem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies
{
    public class FoodItemService(IFoodItemRepository foodItemRepository , IRestaurantRepository restaurantRepository , IMapper mapper ,IUnitOfWork unitOfWork) : IFoodItemService
    {
        //private readonly IFoodItemRepository _foodRepo;
        //private readonly IRepository<FoodItem, int> _baseRepo;
        //private readonly IRepository<Restaurant, int> _restaurantRepo;
        //private readonly IMapper _mapper;

        //public FoodItemService(
        //    IFoodItemRepository foodRepo,
        //    IRepository<FoodItem, int> baseRepo,
        //    IRepository<Restaurant, int> restaurantRepo,
        //    IMapper mapper)
        //{
        //    _foodRepo = foodRepo;
        //    _baseRepo = baseRepo;
        //    _restaurantRepo = restaurantRepo;
        //    _mapper = mapper;
        //}

        // =========================
        // Get All (Pagination + Filter)
        // =========================
        public async Task<PagedResult<FoodItemDto>> GetAllAsync(
            FoodItemFilterDto filter,
            PaginationDto pagination)
        {
            return await foodItemRepository.GetAllAsync(filter, pagination);
        }

        // =========================
        // Get By Id (Details)
        // =========================
        public async Task<FoodItemDetailsDto> GetByIdAsync(int id)
        {
            return await foodItemRepository.GetByIdAsync(id);
        }

        // =========================
        // Create
        // =========================
        public async Task<FoodItemDetailsDto> CreateAsync(
            CreateFoodItemDto dto,
            string userId,
            string role)
        {
            int restaurantId;

            if (role == "Admin")
            {
                if (dto.RestaurantId == null)
                    throw new Exception("Admin must provide RestaurantId");

                restaurantId = dto.RestaurantId.Value;
            }
            else
            {
                var restaurant = await restaurantRepository.GetByUserIdAsync(userId);


                if (restaurant == null)
                    throw new Exception("Restaurant not found");

                restaurantId = restaurant.Id;
            }

            var entity = mapper.Map<FoodItem>(dto);
            entity.RestaurantId = restaurantId;

            await unitOfWork.GetRepository<FoodItem ,int>().AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return await foodItemRepository.GetByIdAsync(entity.Id);
        }

        // =========================
        // Update
        // =========================
        public async Task<FoodItemDetailsDto> UpdateAsync(
            int id,
            UpdateFoodItemDto dto,
            string userId,
            string role)
        {
            var entity = await unitOfWork.GetRepository<FoodItem ,int>().GetByIdAsync(id);

            if (entity == null)
                throw new Exception("Food item not found");

            // Authorization
            if (role != "Admin")
            {
                var restaurant = await restaurantRepository.GetByUserIdAsync(userId);

                if (restaurant == null || entity.RestaurantId != restaurant.Id)
                    throw new Exception("Unauthorized");
            }

            mapper.Map(dto, entity);

            unitOfWork.GetRepository<FoodItem ,int >().Update(entity);
            await unitOfWork.SaveChangesAsync();

            return await foodItemRepository.GetByIdAsync(entity.Id);
        }

        // =========================
        // Delete
        // =========================
        public async Task<bool> DeleteAsync(
            int id,
            string userId,
            string role)
        {
            var entity = await unitOfWork.GetRepository<FoodItem, int>().GetByIdAsync(id);

            if (entity == null)
                throw new Exception("Food item not found");

            // Authorization
            if (role != "Admin")
            {
                var restaurant = await restaurantRepository.GetByUserIdAsync(userId);

                if (restaurant == null || entity.RestaurantId != restaurant.Id)
                    throw new Exception("Unauthorized");
            }

            unitOfWork.GetRepository<FoodItem, int>().Delete(entity);
            await unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
