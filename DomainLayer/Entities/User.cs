using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User"; // Admin or User
        public bool IsActive { get; set; } = true;
    }

}

