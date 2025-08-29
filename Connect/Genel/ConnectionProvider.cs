using Application.Interfaces.Genel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Genel
{
    public class ConnectionProvider : IConnectionProivder
    {
        private readonly string _connectionString;


        public ConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}

    public class XConnection : IXConnectionProvider
    {
        private readonly string _connectionString;

        public XConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("XConnection");
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
       }
    }

  
    public class  SQLXConnection : ISQLXConneciton
    {
        private readonly string _connectionString;

        public SQLXConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

    }

