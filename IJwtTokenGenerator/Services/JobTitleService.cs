using ApplicationLayer.Interfaces;
using DomainLayer.Entities;

namespace ApplicationLayer.Services
{
    public class JobTitleService 
    {
        private readonly IUnitOfWork _uow;

        public JobTitleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IEnumerable<JobTitle>> GetAllAsync()
            => _uow.JobTitles.GetAllAsync();

        public Task<JobTitle?> GetByIdAsync(int id)
            => _uow.JobTitles.GetByIdAsync(id);

        public async Task<bool> CreateAsync(JobTitle job)
        {
            var exists = await _uow.JobTitles.GetByNameAsync(job.Name);
            if (exists != null) return false;

            await _uow.JobTitles.AddAsync(job);
            await _uow.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateAsync( JobTitle job)
        {
            var existing = await _uow.JobTitles.GetByIdAsync(job.Id);
            if (existing == null) return false;

            var duplicate = await _uow.JobTitles.GetByNameAsync(job.Name);
            if (duplicate != null && duplicate.Id != job.Id) return false;

            existing.Name = job.Name;

            _uow.JobTitles.Update(existing);
            await _uow.CompleteAsync();
            return true;
        }
        public async Task<bool> IsJobNameUniqueAsync(string name, int id = 0)
        {
            var existingJob = await _uow.JobTitles.GetByNameAsync(name);
            if (existingJob == null) return true; 
            return existingJob.Id == id; 
        }

        public async Task<bool> IsJobNameExistsAsync(string name, int id = 0)
        {
            var job = await _uow.JobTitles.GetByNameAsync(name);
            if (job == null) return false;

            if (id > 0 && job.Id == id) return false;

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _uow.JobTitles.DeleteAsync(id);
            if (!result) return false;

            await _uow.CompleteAsync();
            return true;
        }

        public async Task<bool> CheckNameUniqueAsync(string name)
        {
            var exists = await _uow.JobTitles.GetByNameAsync(name);
            return exists == null;
        }
    }
}
