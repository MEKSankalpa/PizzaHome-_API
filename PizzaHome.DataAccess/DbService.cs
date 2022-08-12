using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.DataAccess
{
    public class DbService : IDbService
    {
        private readonly IDbConnection _db;

        public DbService(IConfiguration config)
        {
            _db = new NpgsqlConnection(config.GetConnectionString("WebApiDatabase"));
           
        }


       public async Task<int> CreateAndEdit(string command, object param)
        {
            int result;
            result = await _db.ExecuteAsync(command, param);
            return result;
        }

        public async Task<List<T>> GetAll<T>(string command, object param)
        {
            var records = new List<T>();
            records = (await _db.QueryAsync<T>(command, param)).ToList();
            return records;
        }

        public async Task<T> Get<T>(string command, object param)
        {
            T record;
            record = await _db.QueryFirstOrDefaultAsync<T>(command, param);
            return record;
        }
    }
}
