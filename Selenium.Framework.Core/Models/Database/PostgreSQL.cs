using Npgsql;
using Selenium.Framework.Core.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Framework.Core.Models
{
    public class PostgreSQL : Database
    {
        public NpgsqlConnection conn;
        public PostgreSQL (string connectionString)
        {
           this.conn = new NpgsqlConnection("Host=10.45.0.81;Port=5432;User ID=postgres;Password=asecurepassword;Pooling=true;Persist Security Info=true;Database=PayAware");
        }

        public void Query(string sql)
        {
            DatabaseService.PostgreSQLQuery(conn, sql);
        }
    }
}
