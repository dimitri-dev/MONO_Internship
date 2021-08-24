using TheBooks.Models.Common;

namespace TheBooks.Service.Common
{
    public interface ICarsService : IBaseService<ICar, ICreateCarDto, IUpdateCarDto>
    {
    }
}
