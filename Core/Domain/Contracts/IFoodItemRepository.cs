using Shared;
using Shared.Fooditem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IFoodItemRepository
    {
        Task<PagedResult<FoodItemDto>> GetAllAsync(
            FoodItemFilterDto filter,
            PaginationDto pagination);

        Task<FoodItemDetailsDto> GetByIdAsync(int id);
    }
}
