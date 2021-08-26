using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheBooks.Common.Pagination;
using TheBooks.Common.Sort;

namespace TheBooks.Repository.Common
{
    public interface IBaseRepository<ModelT, FilterT>
    {
        Task<ModelT> Create(ModelT dto);
        Task<IEnumerable<ModelT>> GetAll(ISort sort = null, IPagination pagination = null, FilterT filter = default);
        Task<ModelT> Get(Guid id);
        Task<ModelT> Update(Guid id, ModelT dto);
        Task Delete(Guid id);
    }
}
