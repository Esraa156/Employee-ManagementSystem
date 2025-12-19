using ApplicationLayer.DTOs;
using ApplicationLayer.Interfaces;
using DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
 

namespace ApplicationLayer.Services
{
    public class EmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

       
        public EmployeeService(IUnitOfWork uow) => _unitOfWork = uow;

        // Get all employees
        public Task<List<Employee>> GetAllAsync()
        {
            return _unitOfWork.Employees.GetAllAsync();
        }

        // Get employee by id
        public Task<Employee?> GetByIdAsync(int id)
        {
            return _unitOfWork.Employees.GetByIdAsync(id);
        }

        // Add a new employee
        public Task AddAsync(Employee emp)
        {
            return _unitOfWork.Employees.AddAsync(emp);
        }
        public async Task<bool> EmailExistsAsync(string email,int id)
        {
            return await _unitOfWork.Employees.IsEmailUniqueAsync(email,id);
        }

        // Update existing employee
        public Task UpdateAsync(Employee emp)
        {
            return _unitOfWork.Employees.UpdateAsync(emp);
        }

        // Soft delete employee
        public Task SoftDeleteAsync(int id)
        {
            return _unitOfWork.Employees.SoftDeleteAsync(id);
        }
        public async Task<PagedResult<Employee>> SearchAsync(DomainLayer.Entities.filters filters)
        {
          return await _unitOfWork.Employees.SearchAsync(filters);
        }
    }
}
