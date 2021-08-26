using TheBooks.Common.IFilters;
using TheBooks.Models.Common;

namespace TheBooks.Repository.Common
{
    public interface ICarsRepository : IBaseRepository<ICar, ICarFilter>
    {
    }
}
