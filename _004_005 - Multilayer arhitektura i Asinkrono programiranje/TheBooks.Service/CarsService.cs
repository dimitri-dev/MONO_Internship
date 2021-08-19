using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheBooks.Models.Common;
using TheBooks.Repository;
using TheBooks.Repository.Common;
using TheBooks.Service.Common;

namespace TheBooks.Service
{
    public class CarsService : ICarsService
    {
        private ICarsRepository _privateRepositoryCars = new CarsRepository();

        public async Task<ICar> Create(ICreateCarDto dto)
        {
            return await _privateRepositoryCars.Create(dto);
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

        public async Task<IEnumerable<ICar>> GetAll()
        {
            return await _privateRepositoryCars.GetAll();
        }

        public async Task<ICar> GetByID(Guid id)
        {
            return await _privateRepositoryCars.Get(id);
        }

        public async Task<ICar> Update(Guid id, IUpdateCarDto dto)
        {
            return await _privateRepositoryCars.Update(id, dto);
        }
    }
}
