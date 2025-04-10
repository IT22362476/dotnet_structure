using Inv.Application.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Inv.Persistence.Repositories
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "DefaultConnection")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await connection.QueryAsync<T>(storedProcedure, parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
            };

        }
        public async Task<IEnumerable<T>> LoadDataQuery<T, U>(
         string sql,
         U parameters,
         string connectionId = "DefaultConnection")
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
                {
                    return await connection.QueryAsync<T>(sql, parameters, commandTimeout: 120, commandType: CommandType.Text);
                };
            }
            catch (SqlException sqlEx)
            {
                // Log SQL exceptions
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<T>> LoadSPDataQuery<T, U>(
    string sql,
    U parameters,
    string connectionId = "DefaultConnection")
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
                {
                    return await connection.QueryAsync<T>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
                };
            }
            catch (SqlException sqlEx)
            {
                // Log SQL exceptions
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task SaveData<T>(
            string storedProcedure,
            T parameters,
            string connectionId = "DefaultConnection")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            };


        }
        public async Task ExecuteAsync<T>(
       string sqlquery,
       T parameters,
       string connectionId = "DefaultConnection")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await connection.ExecuteAsync(sqlquery, parameters,commandType: CommandType.Text);
            };


        }
        public async Task<T> SingleDataQuery<T, U>(string sql, U parameters, string connectionId = "DefaultConnection")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandTimeout: 120, commandType: CommandType.Text);
            };
        }
    }
}
