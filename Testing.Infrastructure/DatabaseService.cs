using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing.Infrastructure
{

    static public class DatabaseService
    {

        static public string PostgreSQLQuery(NpgsqlConnection conn , string sqlQuery)
        {
            
            conn.Open();

            string escapeSql = sqlQuery; //sqlQuery.Replace("\"","\"\"");

            //NpgsqlCommand cmd = new NpgsqlCommand(@"select ""Id"" from ""OnBoardingStep""", conn);
            NpgsqlCommand cmd = new NpgsqlCommand(escapeSql, conn);

            Console.WriteLine($"send query: {sqlQuery}");

            string result = (string)cmd.ExecuteScalar();

            Console.WriteLine($"get reuslt: {result}");
            
            conn.Close();

            return result;
        }

    }
}
