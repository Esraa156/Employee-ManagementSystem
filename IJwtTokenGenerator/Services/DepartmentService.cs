using DomainLayer.Entities;
using ApplicationLayer.DTOs;
using System;
using ApplicationLayer.Interfaces;

namespace ApplicationLayer.Services
{
    public class DepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork uow) => _unitOfWork = uow;

        public async Task<List<Department>> GetAllAsync() => await _unitOfWork.Departments.GetAllAsync();

        public async Task<Department?> GetByIdAsync(int id) => await _unitOfWork.Departments.GetByIdAsync(id);

        public async Task CreateAsync(Department dept)
        {
            await _unitOfWork.Departments.AddAsync(dept);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> IsNameUniqueAsync(string name)
        {
            var dept = await _unitOfWork.Departments.GetByNameAsync(name.Trim());
            return dept != null;
        }
        public async Task<bool> UpdateAsync(Department dept)
        {
            var existing = await _unitOfWork.Departments.GetByIdAsync(dept.Id);
            if (existing == null) return false;

            existing.Name = dept.Name;
            await _unitOfWork.CompleteAsync();
            return true;
        }

       
            public async Task<bool> DeleteAsync(int id)
            {
                var existing = await _unitOfWork.Departments.GetByIdAsync(id);
                if (existing == null) return false;

                existing.IsActive = false; // Soft Delete
                await _unitOfWork.CompleteAsync();
                return true;
            }

        }
    }



