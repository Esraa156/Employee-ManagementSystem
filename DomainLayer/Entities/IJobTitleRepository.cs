using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{

    
        public interface IJobTitleRepository
        {
            Task<IEnumerable<JobTitle>> GetAllAsync();
            Task<JobTitle?> GetByIdAsync(int id);
            Task<JobTitle?> GetByNameAsync(string name);
            Task AddAsync(JobTitle job);
            void Update(JobTitle job);
            Task<bool> DeleteAsync(int id);
        }
    }

