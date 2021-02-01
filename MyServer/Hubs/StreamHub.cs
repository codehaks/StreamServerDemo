using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyServer.Hubs
{
    public class StreamHub : Hub
    {
        public async IAsyncEnumerable<string> SendDataRow()
        {
            string connectionString = "Data Source=.\\mssql2019;Initial Catalog=AdventureWorks2019;Integrated Security=True";

            string query = "SELECT FirstName,MiddleName,LastName FROM [Person].[Person]";

            using SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (await reader.ReadAsync())
            {
                Console.WriteLine("\t{0}\t{1}\t{2}", reader[0], reader[1], reader[2]);

                await Task.Delay(200);
                yield return reader[0].ToString()+" "+ reader[1].ToString() + " " + reader[2].ToString();
            }

            reader.Close();


        }

    }
}
