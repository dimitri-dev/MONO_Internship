using _003___Baze_podataka.Models.Car;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _003___Baze_podataka.Repositories
{
    public class CarRepository : ICarRepository
    {
        private static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["monoDB"].ConnectionString);
        private static IStudentRepository _privateRepository = new StudentRepository();

        #region CRUD
        public Car Create(CreateCarDto dto)
        {
            Guid ID = Guid.NewGuid();

            Car ret = new Car();
            ret.Id = ID;
            ret.Registration = dto.Registration;
            ret.StudentID = dto.StudentId;

            SqlCommand sql = CreateSqlCommand("INSERT INTO Car VALUES(@Id, @StudentId, @Registration)",
                                             ("@Id", ret.Id), ("@StudentId", ret.StudentID), ("@Registration", ret.Registration));

            _connection.Open();
            sql.ExecuteNonQuery();
            _connection.Close();

            ret.Student = _privateRepository.Get(ret.StudentID);

            return ret;
        }

        public Car Get(Guid id)
        {
            Car ret = null;

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Car A LEFT JOIN Student B ON A.StudentId = B.Id Where A.Id = @Id",
                                             ("@Id", id));

            _connection.Open();
            SqlDataReader sqlReader = sql.ExecuteReader();
            if (sqlReader.HasRows)
            {
                sqlReader.Read();
                ret = MapDataReaderRowToCar(sqlReader);
            }
            _connection.Close();

            return ret;
        }

        public IEnumerable<Car> GetAll()
        {
            ICollection<Car> ret = new List<Car>();

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Car");

            _connection.Open();
            SqlDataReader sqlReader = sql.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Car temp = MapDataReaderRowToCar(sqlReader);
                    ret.Add(temp);
                }
            }
            _connection.Close();

            return ret;
        }

        public Car Update(Guid id, UpdateCarDto dto)
        {
            Car ret = Get(id);
            if (ret == null) return null;

            if (dto.Registration != null) ret.Registration = dto.Registration;
            if (dto.StudentId != null) ret.StudentID = (Guid)dto.StudentId;

            var sql = CreateSqlCommand("UPDATE Car SET Registration = @Registration, StudentId = @StudentId WHERE Id = @Id",
                                      ("@Id", id), ("@Registration", ret.Registration), ("@StudentId", ret.StudentID));

            _connection.Open();
            sql.ExecuteNonQuery();
            _connection.Close();

            return ret;
        }

        public void Delete(Guid id)
        {
            SqlCommand sqlCmd = CreateSqlCommand("DELETE FROM Car WHERE Id = @Id", ("@Id", id));

            _connection.Open();

            int rowsAffected = sqlCmd.ExecuteNonQuery();

            _connection.Close();

            if (rowsAffected == 0)
                throw new ArgumentException("ID not in system.");
        }
        #endregion

        #region SQL Helpers
        private static SqlCommand CreateSqlCommand(string sql, params (string Key, object Value)[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, _connection);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            return cmd;
        }

        private static Car MapDataReaderRowToCar(SqlDataReader reader)
        {
            Car item = new Car();

            item.Id = reader.GetGuid(0);
            item.StudentID = reader.GetGuid(1);
            item.Registration = reader.GetString(2);

            item.Student = _privateRepository.Get(item.StudentID);

            return item;
        }
        #endregion
    }
}