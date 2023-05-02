using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PigAndBaconTannery.Repositories
{
    public abstract class BaseRepository
    {
        private readonly  string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected SqlConnection Connection => new SqlConnection(_connectionString);
    }
}
