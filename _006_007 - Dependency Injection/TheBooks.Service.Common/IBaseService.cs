using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheBooks.Service.Common
{
    public interface IBaseService<ModelT, CreateDtoT, UpdateDtoT>
    {
        Task<ModelT> Create(CreateDtoT dto);
        Task<IEnumerable<ModelT>> GetAll();
        Task<ModelT> GetByID(Guid id);
        Task<ModelT> Update(Guid id, UpdateDtoT dto);
        Task<bool> Delete(Guid id);
    }
}
