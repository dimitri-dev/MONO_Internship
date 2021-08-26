using Guards;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheBooks.Common.IFilters;
using TheBooks.Common.Pagination;
using TheBooks.Common.Sort;
using TheBooks.Models.Common;
using TheBooks.Repository;
using TheBooks.Repository.Common;
using TheBooks.Service.Common;

namespace TheBooks.Service
{
    public class StudentsService : IStudentsService
    {
        private IStudentsRepository _privateRepositoryStudents;

        public StudentsService(IStudentsRepository repository)
        {
            Guard.ArgumentNotNull(() => repository);
            _privateRepositoryStudents = repository;
        }

        public async Task<IStudent> Create(IStudent item)
        {
            return await _privateRepositoryStudents.Create(item);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _privateRepositoryStudents.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<IStudent>> GetAll(ISort sort = null, IPagination pagination = null, IStudentFilter filter = default)
        {
            return await _privateRepositoryStudents.GetAll();
        }

        public async Task<IStudent> GetByID(Guid id)
        {
            return await _privateRepositoryStudents.Get(id);
        }

        public async Task<IStudent> Update(Guid id, IStudent item)
        {
            return await _privateRepositoryStudents.Update(id, item);
        }
    }
}
