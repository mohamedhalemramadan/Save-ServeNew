using Shared;
using Shared.Fooditem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies.Abstractions
{
    public interface IFoodItemService
    {
        Task<PagedResult<FoodItemDto>> GetAllAsync(
            FoodItemFilterDto filter,
            PaginationDto pagination);

        Task<FoodItemDetailsDto> GetByIdAsync(int id);

        Task<FoodItemDetailsDto> CreateAsync(
            CreateFoodItemDto dto,
            string userId,
            string role);

        Task<FoodItemDetailsDto> UpdateAsync(
            int id,
            UpdateFoodItemDto dto,
            string userId,
            string role);

        Task<bool> DeleteAsync(
            int id,
            string userId,
            string role);
    }
}
