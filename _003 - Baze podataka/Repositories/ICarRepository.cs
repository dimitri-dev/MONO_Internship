using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _003___Baze_podataka.Models.Car;

namespace _003___Baze_podataka.Repositories
{
    public interface ICarRepository
    {
        Car Create(CreateCarDto dto);
        IEnumerable<Car> GetAll();
        Car Get(Guid id);
        Car Update(Guid id, UpdateCarDto dto);
        void Delete(Guid id);
    }
}
