using Npgsql;
using System.Data;

namespace Rest_API.Helpers
{
    public class sqlDBHelper : IDisposable
    {
        private NpgsqlConnection connection;
        private string __constr;

        public sqlDBHelper(string pCOnstr)
        {
            __constr = pCOnstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }
        public NpgsqlCommand getNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public void closeConnection()
        {
            connection.Close();
        }

        // Metode wajib untuk IDisposable
        public void Dispose()
        {
            closeConnection();
            connection.Dispose();
        }
    }
}