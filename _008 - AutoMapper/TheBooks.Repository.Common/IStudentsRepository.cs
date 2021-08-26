using TheBooks.Common.IFilters;
using TheBooks.Models.Common;

namespace TheBooks.Repository.Common
{
    public interface IStudentsRepository : IBaseRepository<IStudent, IStudentFilter>
    {
    }
}
