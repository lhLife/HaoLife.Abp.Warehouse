using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
namespace HaoLife.Abp.Warehouse.Cargos;

public interface ICargoCategoryRepository : IRepository<CargoCategory, Guid>
{
    Task<List<CargoCategory>> GetRootsAsync();

    Task<List<CargoCategory>> GetChildsAsync(Guid parentId);

    Task<Dictionary<Guid, int>> GetChildCountAsync(List<Guid> ids);
}
