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

        public async Task<Warehouse[]> GetClosestsByCoordinates(double lat, double lon)
        {            
            var query = $@"
                SELECT TOP 3 *,
                    (6371 * 2 * ASIN(
                        SQRT(
                            POWER(SIN((RADIANS({lat}) - RADIANS(Latitude)) / 2), 2) +
                            COS(RADIANS({lat})) * COS(RADIANS(Latitude)) *
                            POWER(SIN((RADIANS({lon}) - RADIANS(Longitude)) / 2), 2)
                        )
                    )) AS Distance
                FROM Warehouses
                ORDER BY Distance";

            return await Context.Warehouses.FromSqlRaw(query).ToArrayAsync();
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
