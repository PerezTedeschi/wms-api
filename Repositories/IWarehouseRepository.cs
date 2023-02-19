using System.Linq.Expressions;
using wms_api.Entities;

namespace wms_api.Repositories
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetById(int id);
        Task<IEnumerable<Warehouse>> Find(Expression<Func<Warehouse, bool>>? predicate = null);
        Task Add(Warehouse warehouse);
        Task Delete(Warehouse warehouse);
    }
}

