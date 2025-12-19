using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    using DomainLayer.Entities;

    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department?> GetByNameAsync(string name);

        Task AddAsync(Department dept);
        Task UpdateAsync(Department dept);
        Task SoftDeleteAsync(int id);
    }

}
