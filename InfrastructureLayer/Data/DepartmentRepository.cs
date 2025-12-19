using DomainLayer.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data
{
  
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) => _context = context;

        public async Task<List<Department>> GetAllAsync() => await _context.Departments.ToListAsync();

        public async Task<Department?> GetByIdAsync(int id) => await _context.Departments.FindAsync(id);
        public async Task<Department?> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            return await _context.Departments
                                 .FirstOrDefaultAsync(d => d.Name.ToLower() == name.Trim().ToLower());
        }

        public async Task AddAsync(Department dept) => await _context.Departments.AddAsync(dept);

        public async Task UpdateAsync(Department dept)
        {
            var existing = await _context.Departments.FindAsync(dept.Id);
            if (existing != null) existing.Name = dept.Name;
        }

        public async Task SoftDeleteAsync(int id)
        {
            var existing = await _context.Departments.FindAsync(id);
            if (existing != null) existing.IsActive = false;
        }
    }

}
