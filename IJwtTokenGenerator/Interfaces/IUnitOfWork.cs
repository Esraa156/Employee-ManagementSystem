using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces;

using DomainLayer.Entities;

public interface IUnitOfWork : IDisposable
{
    IDepartmentRepository Departments { get; }
    IJobTitleRepository JobTitles { get; }
    IEmployeeRepository Employees { get; }

    Task<int> CompleteAsync(); 
}
