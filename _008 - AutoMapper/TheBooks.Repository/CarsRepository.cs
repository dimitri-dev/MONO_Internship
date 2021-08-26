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
    public class CarsRepository : ICarsRepository
    {
        private static SqlConnection _connection;
        private IMapper _mapper;

        public CarsRepository(SqlConnection connection, IMapper mapper)
        {
            Guard.ArgumentNotNull(() => connection);
            _connection = connection;

            Guard.ArgumentNotNull(() => mapper);
            _mapper = mapper;
        }

        #region CRUD
        public async Task<ICar> Create(ICar item)
        {
            item.Id = Guid.NewGuid();

            SqlCommand sql = CreateSqlCommand("INSERT INTO Car VALUES(@Id, @StudentId, @Registration)",
                                             ("@Id", item.Id), ("@StudentId", item.StudentID), ("@Registration", item.Registration));

            _connection.Open();
            await sql.ExecuteNonQueryAsync();
            _connection.Close();

            return item;
        }

        public async Task<ICar> Get(Guid id)
        {
            ICar ret = null;

            SqlCommand sql = CreateSqlCommand("SELECT * FROM Car A LEFT JOIN Student B ON A.StudentId = B.Id Where A.Id = @Id",
                                             ("@Id", id));

            _connection.Open();
            SqlDataReader sqlReader = await sql.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                await sqlReader.ReadAsync();
                ret = _mapper.Map<IDataRecord, Car>(sqlReader);
            }
            _connection.Close();

            return ret;
        }

        public async Task<IEnumerable<ICar>> GetAll(ISort sort = null, IPagination pagination = null, ICarFilter filter = default)
        {
            ICollection<ICar> ret = new List<ICar>();
            List<(string key, object value)> sqlParams = new List<(string key, object value)>();

            string sqlCommand = "SELECT * FROM Car A LEFT JOIN Student B ON A.StudentId = B.Id";

            if (filter?.Search != null)
            {
                sqlCommand += " Where Registration LIKE @Search";
                sqlParams.Add(("@Search", $"%{filter.Search}%"));
            }

            if (filter?.StudentID != null)
            {
                if (sqlCommand.Contains("@Search")) sqlCommand += " AND";
                sqlCommand += " Where StudentID = @StudentID";
                sqlParams.Add(("@StudentID", $"{filter.StudentID}"));
            }

            sqlCommand += $" ORDER BY {sort?.SortBy ?? "Registration"} {sort.Order.ToUpper()}";

            if (pagination?.PageNumber != null)
            {
                int offset = ((int)pagination.PageNumber - 1) * (int)pagination.PageSize;
                sqlCommand += $" OFFSET {offset} ROWS FETCH NEXT {(int)pagination.PageSize} ROWS ONLY";
            }

            SqlCommand sql = CreateSqlCommand(sqlCommand, sqlParams.ToArray());

            await _connection.OpenAsync();
            SqlDataReader sqlReader = await sql.ExecuteReaderAsync();

            if (sqlReader.HasRows)
            {
                while (await sqlReader.ReadAsync())
                {
                    ICar temp = _mapper.Map<IDataRecord, Car>(sqlReader);
                    ret.Add(temp);
                }
            }

            sqlReader.Close();
            _connection.Close();

            return ret;
        }

        public async Task<ICar> Update(Guid id, ICar item)
        {
            var sql = CreateSqlCommand("UPDATE Car SET Registration = @Registration, StudentId = @StudentId WHERE Id = @Id",
                                      ("@Id", id), ("@Registration", item.Registration), ("@StudentId", item.StudentID));

            _connection.Open();
            await sql.ExecuteNonQueryAsync();
            _connection.Close();

            return item;
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

        #endregion
    }
}
