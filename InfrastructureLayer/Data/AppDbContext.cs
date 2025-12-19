using Microsoft.EntityFrameworkCore;
using DomainLayer;
using DomainLayer.Entities;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Entities (Tables)
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global Filter → Soft Delete
            modelBuilder.Entity<User>().HasQueryFilter(x => x.IsActive);

            // Soft Delete filter
            modelBuilder.Entity<Department>().HasQueryFilter(d => d.IsActive);
            modelBuilder.Entity<JobTitle>().HasQueryFilter(j => j.IsActive);
            modelBuilder.Entity<Employee>().HasQueryFilter(e => e.IsActive);



            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Department>().HasIndex(d => d.Name).IsUnique();
            modelBuilder.Entity<JobTitle>().HasIndex(j => j.Name).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();


            // Seed Data (Optional)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Admin User",
                    Email = "admin@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                    Role = "Admin",
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    FullName = "Normal User",
                    Email = "user@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123"),
                    Role = "User",
                    IsActive = true
                }
            );
            modelBuilder.Entity<Department>().HasData(
      new Department { Id = 1, Name = "Human Resources", IsActive = true },
      new Department { Id = 2, Name = "IT", IsActive = true },
      new Department { Id = 3, Name = "Finance", IsActive = true },
      new Department { Id = 4, Name = "Operations", IsActive = true }
  );
            modelBuilder.Entity<JobTitle>().HasData(
      new JobTitle { Id = 1, Name = "Software Engineer", IsActive = true },
      new JobTitle { Id = 2, Name = "HR Specialist", IsActive = true },
      new JobTitle { Id = 3, Name = "Accountant", IsActive = true },
      new JobTitle { Id = 4, Name = "Project Manager", IsActive = true }
  );
        }
    }
}
