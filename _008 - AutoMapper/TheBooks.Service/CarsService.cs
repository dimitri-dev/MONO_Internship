using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheBooks.Models.Common;
using TheBooks.Repository.Common;
using TheBooks.Service.Common;
using TheBooks.Common.Sort;
using TheBooks.Common.Pagination;
using TheBooks.Common.IFilters;
using Guards;


namespace TheBooks.Service
{
    public class CarsService : ICarsService
    {
        private ICarsRepository _privateRepositoryCars;

        public CarsService(ICarsRepository repository)
        {
            Guard.ArgumentNotNull(() => repository);
            _privateRepositoryCars = repository;
        }

        public async Task<ICar> Create(ICar item)
        {
            return await _privateRepositoryCars.Create(item);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _privateRepositoryCars.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ICar>> GetAll(ISort sort = null, IPagination pagination = null, ICarFilter filter = null)
        {
            return await _privateRepositoryCars.GetAll(sort, pagination, filter);
        }

        public async Task<ICar> GetByID(Guid id)
        {
            return await _privateRepositoryCars.Get(id);
        }

        public async Task<ICar> Update(Guid id, ICar item)
        {
            return await _privateRepositoryCars.Update(id, item);
        }
    }
}
