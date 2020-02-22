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
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mypub;Integrated Security=True";

            string query = "SELECT * FROM Payments";

            using SqlConnection connection =
            new SqlConnection(connectionString);
            // Create the Command and Parameter objects.
            SqlCommand command = new SqlCommand(query, connection);


            // Open the connection in a try/catch block. 
            // Create and execute the DataReader, writing the result
            // set to the console window.

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("\t{0}\t{1}\t{2}",
                    reader[0], reader[1], reader[2]);

                await Task.Delay(1000);
                yield return reader[1].ToString();
            }
            reader.Close();

            //for (int i = 0; i < 10; i++)
            //{
            //    await Task.Delay(1000);
            //    yield return i.ToString();
            //}
        }
        
    }
}
