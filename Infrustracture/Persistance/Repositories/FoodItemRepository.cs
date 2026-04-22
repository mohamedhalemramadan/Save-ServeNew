using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;
using Shared;
using Shared.Fooditem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public FoodItemRepository(StoreDBContext context,IMapper  mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<FoodItemDto>> GetAllAsync(
            FoodItemFilterDto filter,
            PaginationDto pagination)
        {
            var query = _context.FoodItems
                .Include(x => x.Restaurant)
                .Include(x => x.Category)
                .AsQueryable();

            // =========================
            // Filtering
            // =========================
            if (!string.IsNullOrEmpty(filter?.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter?.CategoryId != null)
                query = query.Where(x => x.CategoryId == filter.CategoryId);

            if (filter?.MinPrice != null)
                query = query.Where(x => x.Price >= filter.MinPrice);

            if (filter?.MaxPrice != null)
                query = query.Where(x => x.Price <= filter.MaxPrice);

            if (!string.IsNullOrEmpty(filter?.Status))
                query = query.Where(x => x.Status == filter.Status);

            // =========================
            // Total Count
            // =========================
            var totalCount = await query.CountAsync();

            // =========================
            // Pagination
            // =========================
            var data = await query
                .OrderByDescending(x => x.Id)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ProjectTo<FoodItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<FoodItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<FoodItemDetailsDto> GetByIdAsync(int id)
        {
            var item = await _context.FoodItems
                .Include(x => x.Restaurant)
                .Include(x => x.Category)
                .Where(x => x.Id == id)
                .ProjectTo<FoodItemDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (item == null)
                throw new Exception("Food item not found");

            return item;
        }
    }
}
