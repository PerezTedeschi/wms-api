using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using wms_api.Entities;

namespace wms_api.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private ApplicationDbContext Context { get; set; }

        public WarehouseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Warehouse?> GetById(int id)
        {
            return await Context.Warehouses.FindAsync(id);
        }

        public async Task<IEnumerable<Warehouse>> Find(Expression<Func<Warehouse, bool>>? predicate = null)
        {
            if (predicate == null)
                return await Context.Warehouses.ToListAsync();

            return await Context.Warehouses.Where(predicate).ToListAsync();
        }

        public async Task Add(Warehouse warehouse)
        {
            await Context.Warehouses.AddAsync(warehouse);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Warehouse warehouse)
        {
            Context.Warehouses.Remove(warehouse);
            await Context.SaveChangesAsync();
        }
    }
}
