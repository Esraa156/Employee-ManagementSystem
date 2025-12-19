using DomainLayer.Entities;
using Infrastructure.Data;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.DTOs;
namespace InfrastructureLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employee emp)
        {
            await _context.Employees.AddAsync(emp);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee emp)
        {
            _context.Employees.Update(emp);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                emp.IsActive = false;
                _context.Employees.Update(emp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<Employee>> SearchAsync(DomainLayer.Entities.filters filters)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Name))
                query = query.Where(e => e.FullName.Contains(filters.Name));

            if (!string.IsNullOrEmpty(filters.Email))
                query = query.Where(e => e.Email.Contains(filters.Email));

            if (!string.IsNullOrEmpty(filters.Mobile))
                query = query.Where(e => e.Mobile.Contains(filters.Mobile));

            if (filters.DepartmentId.HasValue)
                query = query.Where(e => e.DepartmentId == filters.DepartmentId.Value);

            if (filters.JobTitleId.HasValue)
                query = query.Where(e => e.JobTitleId == filters.JobTitleId.Value);

            if (filters.DobFrom.HasValue)
                query = query.Where(e => e.DateOfBirth >= filters.DobFrom.Value);

            if (filters.DobTo.HasValue)
                query = query.Where(e => e.DateOfBirth <= filters.DobTo.Value);

            // Sorting
            query = (filters.sortColumn, filters.sortDir.ToLower()) switch
            {
                ("FullName", "asc") => query.OrderBy(e => e.FullName),
                ("FullName", "desc") => query.OrderByDescending(e => e.FullName),
                ("Email", "asc") => query.OrderBy(e => e.Email),
                ("Email", "desc") => query.OrderByDescending(e => e.Email),
                ("DateOfBirth", "asc") => query.OrderBy(e => e.DateOfBirth),
                ("DateOfBirth", "desc") => query.OrderByDescending(e => e.DateOfBirth),
                _ => query.OrderBy(e => e.FullName)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filters.PageNumber - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();

            return new PagedResult<Employee>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize
            };
        }

        public async Task<bool> IsEmailUniqueAsync(string email, int excludeId = 0)
        {
            return await _context.Employees.AnyAsync(e => e.Email == email && e.Id != excludeId);
        }

        public async Task<bool> IsMobileUniqueAsync(string mobile, int excludeId = 0)
        {
            return !await _context.Employees.AnyAsync(e => e.Mobile == mobile && e.Id != excludeId);
        }
    }
}
