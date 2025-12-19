using ApplicationLayer.Interfaces;
using DomainLayer.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Where(u => u.Email == email && u.IsActive) // Soft delete respected
            .FirstOrDefaultAsync();
    }
}
