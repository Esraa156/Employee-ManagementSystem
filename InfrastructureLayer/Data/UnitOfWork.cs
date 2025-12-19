using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data
{
    using ApplicationLayer.Interfaces;
    using DomainLayer.Entities;
    using Infrastructure.Data;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IDepartmentRepository Departments { get; }
        public IJobTitleRepository JobTitles { get; }
        public IEmployeeRepository Employees { get; }

        public UnitOfWork(AppDbContext context,
                          IDepartmentRepository departmentRepo,
                          IJobTitleRepository jobTitleRepo,
                          IEmployeeRepository employeeRepo)
        {
            _context = context;
            Departments = departmentRepo;
            JobTitles = jobTitleRepo;
            Employees = employeeRepo;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
    }

}
