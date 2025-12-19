using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public  class GetAllEmployeeDto
    {

        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, RegularExpression(@"^\d{11}$")]
        public string Mobile { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string JobTitle { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
