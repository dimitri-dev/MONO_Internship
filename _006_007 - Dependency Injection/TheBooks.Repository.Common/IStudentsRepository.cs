using TheBooks.Models.Common;

namespace TheBooks.Repository.Common
{
    public interface IStudentsRepository : IBaseRepository<IStudent, ICreateStudentDto, IUpdateStudentDto>
    {
    }
}
