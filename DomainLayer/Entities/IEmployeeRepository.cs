using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Serialization; // Required for JsonPropertyName

namespace DomainLayer.Entities

{
    public class filters
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public DateTime? DobFrom { get; set; }
        public DateTime? DobTo { get; set; }

        public string? sortColumn { get; set; }

        public string? sortDir { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }



public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();

        Task<Employee?> GetByIdAsync(int id);

        Task AddAsync(Employee emp);

        Task UpdateAsync(Employee emp);

        Task SoftDeleteAsync(int id);

        Task<PagedResult<Employee>> SearchAsync(
        filters employeeSearchFilters
        );

        Task<bool> IsEmailUniqueAsync(string email, int excludeId = 0);
        Task<bool> IsMobileUniqueAsync(string mobile, int excludeId = 0);
    }
}
