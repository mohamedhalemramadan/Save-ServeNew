using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Dates;

namespace Persistance.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDBContext _storeDBContext;

        public GenericRepository(StoreDBContext storeDBContext)
        {
            _storeDBContext = storeDBContext;
        }
        public async Task AddAsync(TEntity entity)
       => await _storeDBContext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity)
         => _storeDBContext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity)
        => _storeDBContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false)
       => TrackChanges ? await _storeDBContext.Set<TEntity>().ToListAsync()
            : await _storeDBContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey Id)
        => await _storeDBContext.Set<TEntity>().FindAsync(Id);
    }
}
