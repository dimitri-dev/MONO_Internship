using Guards;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBooks.Models;
using TheBooks.Models.Common;
using TheBooks.Repository.Common;

namespace TheBooks.Repository
{
    public class CarsRepository : ICarsRepository
    {
        private static IStudentsRepository _privateRepository;

        public CarsRepository(IStudentsRepository repositoryLink)
        {
            Guard.ArgumentNotNull(() => repositoryLink);
            _privateRepository = repositoryLink;
        }
        
        private static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["monoDB"].ConnectionString);

        #region CRUD
        public async Task<ICar> Create(ICreateCarDto dto)
        {
            Guid ID = Guid.NewGuid();

            ICar ret = new Car();
            ret.Id = ID;
            ret.Registration = dto.Registration;
            ret.StudentID = dto.StudentId;

            SqlCommand sql = CreateSqlCommand("INSERT INTO Car VALUES(@Id, @StudentId, @Registration)",
                                             ("@Id", ret.Id), ("@StudentId", ret.StudentID), ("@Registration", ret.Registration));

            _connection.Open();
            await sql.ExecuteNonQueryAsync();
            _connection.Close();

            ret.Student = await _privateRepository.Get(ret.StudentID);

            return ret;
        }

        public async Task<ICar> Get(Guid id)
        {
            ICar ret = null;

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Car A LEFT JOIN Student B ON A.StudentId = B.Id Where A.Id = @Id",
                                             ("@Id", id));

            _connection.Open();
            SqlDataReader sqlReader = await sql .ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                sqlReader.Read();
                ret = await MapDataReaderRowToCar(sqlReader);
            }
            _connection.Close();

            return ret;
        }

        public async Task<IEnumerable<ICar>> GetAll()
        {
            ICollection<ICar> ret = new List<ICar>();

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Car");

            _connection.Open();
            SqlDataReader sqlReader = await sql.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    ICar temp = await MapDataReaderRowToCar(sqlReader);
                    ret.Add(temp);
                }
            }
            _connection.Close();

            return ret;
        }

        public async Task<ICar> Update(Guid id, IUpdateCarDto dto)
        {
            ICar ret = await Get(id);
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

        public async Task Delete(Guid id)
        {
            SqlCommand sqlCmd = CreateSqlCommand("DELETE FROM Car WHERE Id = @Id", ("@Id", id));

            _connection.Open();

            int rowsAffected = await sqlCmd.ExecuteNonQueryAsync();

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

        private async static Task<ICar> MapDataReaderRowToCar(SqlDataReader reader)
        {
            ICar item = new Car();

            item.Id = reader.GetGuid(0);
            item.StudentID = reader.GetGuid(1);
            item.Registration = reader.GetString(2);

            item.Student = await _privateRepository.Get(item.StudentID);

            return item;
        }
        #endregion
    }
}
