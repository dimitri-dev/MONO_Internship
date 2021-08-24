using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TheBooks.Repository;
using TheBooks.Repository.Common;
using TheBooks.Service.Common;

namespace TheBooks.Service
{
    public class ServiceHandler
    {
        private static readonly ServiceHandler _instance = new ServiceHandler();

        private static IStudentsRepository _studentRepo = new StudentsRepository();
        private IStudentsService _studentService;

        private static ICarsRepository _carsRepo = new CarsRepository(_studentRepo);
        private ICarsService _carService;

        private ServiceHandler()
        {
            _studentService = new StudentsService(_studentRepo);
            _carService = new CarsService(_carsRepo);
        }

        public ServiceHandler Instance { get => _instance; }
        public T Service<T>() => typeof(T) == typeof(CarsService) ? (T)(object)_carService :
                                 typeof(T) == typeof(StudentsService) ? (T)(object)_studentService :
                                 default(T);

    }
}
