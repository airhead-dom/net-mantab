using Microsoft.Data.SqlClient;

namespace NetMantab.Data
{
    public class AppADOConnection {
        private readonly string _connString;

        public AppADOConnection(string connString) {
                _connString = connString;
        }

        public SqlConnection GetConnection() {
            return new SqlConnection(_connString);
        }
    }
}