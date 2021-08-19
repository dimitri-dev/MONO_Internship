using TheBooks.Models.Common;

namespace TheBooks.Repository.Common
{
    public interface ICarsRepository : IBaseRepository<ICar, ICreateCarDto, IUpdateCarDto>
    {
    }
}
