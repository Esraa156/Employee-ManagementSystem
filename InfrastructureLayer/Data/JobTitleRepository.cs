using DomainLayer.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Data
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly AppDbContext _context;

        public JobTitleRepository(AppDbContext context)
        {
            _context = context;
        }
      

        public async Task<IEnumerable<JobTitle>> GetAllAsync()
        {
            return await _context.JobTitles.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<JobTitle?> GetByIdAsync(int id)
        {
            return await _context.JobTitles.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task<JobTitle?> GetByNameAsync(string name)
        {
            return await _context.JobTitles.FirstOrDefaultAsync(x => x.Name == name && x.IsActive);
        }

        public async Task AddAsync(JobTitle job)
        {
            await _context.JobTitles.AddAsync(job);
        }

        public void Update(JobTitle job)
        {
            _context.JobTitles.Update(job);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var job = await _context.JobTitles.FindAsync(id);
            if (job == null) return false;

            job.IsActive = false;
            return true;
        }

     
    }
}
