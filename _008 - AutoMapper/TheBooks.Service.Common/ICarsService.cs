using TheBooks.Common.IFilters;
using TheBooks.Models.Common;

namespace TheBooks.Service.Common
{
    public interface ICarsService : IBaseService<ICar, ICarFilter>
    {
    }
}
