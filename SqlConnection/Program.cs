// See https://aka.ms/new-console-template for more information
using System;
using Microsoft.Data.SqlClient;
 namespace sqlconnection
{
    class SqlConnections
    {
        static void Main(String[] args)
        {
            try
            {
                string src = "Server = localhost ; Database = SMS ; Trusted_Connection = True ; TrustServerCertificate = True;";
                SqlConnection con = new SqlConnection(src);
                con.Open();
                Console.WriteLine("Connection Succesful ");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

    }
}
