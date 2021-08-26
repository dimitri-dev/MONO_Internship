using AutoMapper;
using Guards;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TheBooks.Common.IFilters;
using TheBooks.Common.Pagination;
using TheBooks.Common.Sort;
using TheBooks.Models;
using TheBooks.Models.Common;
using TheBooks.Repository.Common;

namespace TheBooks.Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        private static SqlConnection _connection;
        private IMapper _mapper;

        public StudentsRepository(SqlConnection connection, IMapper mapper)
        {
            Guard.ArgumentNotNull(() => connection);
            _connection = connection;

            Guard.ArgumentNotNull(() => mapper);
            _mapper = mapper;
        }

        #region CRUD
        public async Task<IStudent> Create(IStudent item)
        {
            item.Id = Guid.NewGuid();

            SqlCommand sql = CreateSqlCommand("INSERT INTO Student VALUES(@Id, @Name, @Surname, @Gender)",
                                             ("@Id", item.Id), ("@Name", item.Name), ("@Surname", item.Surname), ("@Gender", item.Gender));

            _connection.Open();
            await sql.ExecuteNonQueryAsync();
            _connection.Close();

            return item;
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
                ret = _mapper.Map<IDataRecord, Student>(sqlReader);
            }
            _connection.Close();

            return ret;
        }

        public async Task<IEnumerable<IStudent>> GetAll(ISort sort = null, IPagination pagination = null, IStudentFilter filter = default)
        {
            ICollection<IStudent> ret = new List<IStudent>();
            List<(string key, object value)> sqlParams = new List<(string key, object value)>();

            string sqlCommand = "SELECT * FROM Student";

            if (filter?.Search != null)
            {
                sqlCommand += " Where Name LIKE @Search OR Gender LIKE @Search";
                sqlParams.Add(("@Search", $"%{filter.Search}%"));
            }

            if (filter?.Gender != null)
            {
                if (sqlCommand.Contains("@Search")) sqlCommand += " AND";
                sqlCommand += " Where Gender = @Gender";
                sqlParams.Add(("@Gender", $"{filter.Gender}"));
            }

            sqlCommand += $" ORDER BY {sort?.SortBy ?? "Name"} {sort?.Order.ToUpper() ?? "ASC"}";

            if (pagination?.PageNumber != null)
            {
                int offset = ((int)pagination.PageNumber - 1) * (int)pagination.PageSize;
                sqlCommand += $" OFFSET {offset} ROWS FETCH NEXT {(int)pagination.PageSize} ROWS ONLY";
            }

            SqlCommand sql = CreateSqlCommand(sqlCommand, sqlParams.ToArray() ?? null);

            await _connection.OpenAsync();
            SqlDataReader sqlReader = await sql.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                while (await sqlReader.ReadAsync())
                {
                    IStudent temp = _mapper.Map<IDataRecord, Student>(sqlReader);
                    ret.Add(temp);
                }
            }
            _connection.Close();
            return ret;
        }

        public async Task<IStudent> Update(Guid id, IStudent item)
        {
            var sql = CreateSqlCommand("UPDATE Student SET Name = @Name, Surname = @Surname, Gender = @Gender WHERE Id = @Id",
                ("@Id", id), ("@Name", item.Name), ("@Surname", item.Surname), ("@Gender", item.Gender));

            _connection.Open();
            await sql.ExecuteNonQueryAsync();
            _connection.Close();

            return item;
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
        #endregion
    }
}
