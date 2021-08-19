using TheBooks.Models.Common;

namespace TheBooks.Service.Common
{
    public interface IStudentsService : IBaseService<IStudent, ICreateStudentDto, IUpdateStudentDto>
    {
    }
}
