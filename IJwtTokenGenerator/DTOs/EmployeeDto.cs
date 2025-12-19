using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs
{
    public class EmployeeDto
    {
        [Required, MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, RegularExpression(@"^\d{11}$")]
        public string Mobile { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int JobTitleId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
