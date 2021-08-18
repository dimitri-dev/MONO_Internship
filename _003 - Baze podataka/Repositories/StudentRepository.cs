using _003___Baze_podataka.Models.Student;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _003___Baze_podataka.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["monoDB"].ConnectionString);

        #region CRUD
        public Student Create(CreateStudentDto dto)
        {
            Guid ID = Guid.NewGuid();
            Student ret = new Student();
            ret.Id = ID;
            ret.Name = dto.Name;
            ret.Surname = dto.Surname;
            ret.Gender = dto.Gender;

            SqlCommand sql = CreateSqlCommand("INSERT INTO Student VALUES(@Id, @Name, @Surname, @Gender)",
                                             ("@Id", ret.Id), ("@Name", ret.Name), ("@Surname", ret.Surname), ("@Gender", ret.Gender));

            _connection.Open();
            sql.ExecuteNonQuery();
            _connection.Close();

            return ret;
        }

        public Student Get(Guid id)
        {
            Student ret = null;

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Student Where Id = @Id",
                                             ("@Id", id));
            
            _connection.Open();
            SqlDataReader sqlReader = sql.ExecuteReader();
            if (sqlReader.HasRows)
            {
                sqlReader.Read();
                ret = MapDataReaderRowToStudent(sqlReader);
            }
            _connection.Close();

            return ret;
        }

        public IEnumerable<Student> GetAll()
        {
            ICollection<Student> ret = new List<Student>();

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Student");

            _connection.Open();
            SqlDataReader sqlReader = sql.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Student temp = MapDataReaderRowToStudent(sqlReader);
                    ret.Add(temp);
                }
            }
            _connection.Close();
            return ret;
        }

        public Student Update(Guid id, UpdateStudentDto dto)
        {
            Student ret = Get(id);
            if (ret == null) return null;

            if (dto.Name != null) ret.Name = dto.Name;
            if (dto.Surname != null) ret.Surname = dto.Surname;
            if (dto.Gender != null) ret.Gender = dto.Gender;

            var sql = CreateSqlCommand("UPDATE Student SET Name = @Name, Surname = @Surname, Gender = @Gender WHERE Id = @Id",
                ("@Id", id), ("@Name", ret.Name), ("@Surname", ret.Surname), ("@Gender", ret.Gender));

            _connection.Open();
            sql.ExecuteNonQuery();
            _connection.Close();

            return ret;
        }

        public void Delete(Guid id)
        {
            SqlCommand sqlCmd = CreateSqlCommand("DELETE FROM Student WHERE Id = @Id", ("@Id", id));
            
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

        private static Student MapDataReaderRowToStudent(SqlDataReader reader)
        {
            Student item = new Student();
            item.Id = reader.GetGuid(0);
            item.Name = reader.GetString(1);
            item.Surname = reader.GetString(2);
            item.Gender = reader.GetString(3);
            return item;
        }
        #endregion
    }
}