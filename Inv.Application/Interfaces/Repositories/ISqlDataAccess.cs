using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Interfaces.Repositories
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "DefaultConnection");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "DefaultConnection");
        Task ExecuteAsync<T>(string sqlquery, T parameters, string connectionId = "DefaultConnection");
        Task<IEnumerable<T>> LoadDataQuery<T, U>(string sql, U parameters, string connectionId = "DefaultConnection");
        Task<T> SingleDataQuery<T, U>(string sql, U parameters, string connectionId = "DefaultConnection");
        Task<IEnumerable<T>> LoadSPDataQuery<T, U>(string sql, U parameters, string connectionId = "DefaultConnection");
    }
}
