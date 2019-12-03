using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Framework.Core.Helper
{

    static public class DatabaseService
    {

        static public void PostgreSQLQuery(NpgsqlConnection conn , string sqlQuery)
        {
            
            conn.Open();

            string escapeSql = sqlQuery; //sqlQuery.Replace("\"","\"\"");

            //NpgsqlCommand cmd = new NpgsqlCommand(@"select ""Id"" from ""OnBoardingStep""", conn);
            NpgsqlCommand cmd = new NpgsqlCommand(escapeSql, conn);

            // Execute a query
            NpgsqlDataReader dr = cmd.ExecuteReader();

            // Read all rows and output the first column in each row
            while (dr.Read())
                Console.Write("{0},{1},{2},{3}\n", dr[0], dr[1], dr[2], dr[3]);
        }

    }
}
