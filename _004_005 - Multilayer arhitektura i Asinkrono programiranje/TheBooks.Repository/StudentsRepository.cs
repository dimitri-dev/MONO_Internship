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
    public class StudentsRepository : IStudentsRepository
    {
        private static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["monoDB"].ConnectionString);

        #region CRUD
        public async Task<IStudent> Create(ICreateStudentDto dto)
        {
            Guid ID = Guid.NewGuid();
            IStudent ret = new Student();
            ret.Id = ID;
            ret.Name = dto.Name;
            ret.Surname = dto.Surname;
            ret.Gender = dto.Gender;

            SqlCommand sql = CreateSqlCommand("INSERT INTO Student VALUES(@Id, @Name, @Surname, @Gender)",
                                             ("@Id", ret.Id), ("@Name", ret.Name), ("@Surname", ret.Surname), ("@Gender", ret.Gender));

            _connection.Open();
            await sql.ExecuteNonQueryAsync();
            _connection.Close();

            return ret;
        }

        public async Task<IStudent> Get(Guid id)
        {
            IStudent ret = null;

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Student Where Id = @Id",
                                             ("@Id", id));

            _connection.Open();
            SqlDataReader sqlReader = await sql.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                sqlReader.Read();
                ret = MapDataReaderRowToStudent(sqlReader);
            }
            _connection.Close();

            return ret;
        }

        public async Task<IEnumerable<IStudent>> GetAll()
        {
            ICollection<IStudent> ret = new List<IStudent>();

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Student");

            _connection.Open();
            SqlDataReader sqlReader = await sql.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    IStudent temp = MapDataReaderRowToStudent(sqlReader);
                    ret.Add(temp);
                }
            }
            _connection.Close();
            return ret;
        }

        public async Task<IStudent> Update(Guid id, IUpdateStudentDto dto)
        {
            IStudent ret = await Get(id);
            if (ret == null) return null;

            if (dto.Name != null) ret.Name = dto.Name;
            if (dto.Surname != null) ret.Surname = dto.Surname;
            if (dto.Gender != null) ret.Gender = dto.Gender;

            var sql = CreateSqlCommand("UPDATE Student SET Name = @Name, Surname = @Surname, Gender = @Gender WHERE Id = @Id",
                ("@Id", id), ("@Name", ret.Name), ("@Surname", ret.Surname), ("@Gender", ret.Gender));

            _connection.Open();
            await sql.ExecuteNonQueryAsync();
            _connection.Close();

            return ret;
        }

        public async Task Delete(Guid id)
        {
            SqlCommand sqlCmd = CreateSqlCommand("DELETE FROM Student WHERE Id = @Id", ("@Id", id));

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

        private static IStudent MapDataReaderRowToStudent(SqlDataReader reader)
        {
            IStudent item = new Student();
            item.Id = reader.GetGuid(0);
            item.Name = reader.GetString(1);
            item.Surname = reader.GetString(2);
            item.Gender = reader.GetString(3);
            return item;
        }
        #endregion
    }
}
