using Microsoft.Data.SqlClient;
namespace Common.DAL.DataBase
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory()
        {
            _connectionString = "Server = localhost ; Database = SMS ; Trusted_Connection = True ; TrustServerCertificate = True;";
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);

        }
    }
}
