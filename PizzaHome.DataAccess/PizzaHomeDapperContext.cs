using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.DataAccess
{
    public class PizzaHomeDapperContext 
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public PizzaHomeDapperContext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("WebApiDatabase");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
