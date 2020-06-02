using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using Testing.Infrastructure;

namespace Selenium.Framework.Core.Models
{
    public class PostgreSQL : Database
    {
        private NpgsqlConnection conn;
        public PostgreSQL (string connectionString)
        {
           conn = new NpgsqlConnection(connectionString);
        }

        public string Query(string sql)
        {
            return DatabaseService.PostgreSQLQuery(conn, sql);
        }
    }
}
