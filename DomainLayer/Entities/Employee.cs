using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, RegularExpression(@"^\d{11}$")]
        public string Mobile { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        // Relationships
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; } = null!;

        public bool IsActive { get; set; } = true; // Soft delete
    }

}
