using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheBooks.Repository.Common
{
    public interface IBaseRepository<ModelT, CreateDtoT, UpdateDtoT>
    {
        Task<ModelT> Create(CreateDtoT dto);
        Task<IEnumerable<ModelT>> GetAll();
        Task<ModelT> Get(Guid id);
        Task<ModelT> Update(Guid id, UpdateDtoT dto);
        Task Delete(Guid id);
    }
}
