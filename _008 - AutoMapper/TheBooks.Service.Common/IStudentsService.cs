using TheBooks.Common.IFilters;
using TheBooks.Models.Common;

namespace TheBooks.Service.Common
{
    public interface IStudentsService : IBaseService<IStudent, IStudentFilter>
    {
    }
}
