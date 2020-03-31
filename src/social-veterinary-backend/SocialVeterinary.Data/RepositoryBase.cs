namespace SocialVeterinary.Data
{
    using System.Data.Common;

    using Dapper;

    using Microsoft.Extensions.Configuration;

    using MySql.Data.MySqlClient;

    public class RepositoryBase
    {
        private const string ConnectionStringKey = "Database";

        private readonly string _connectionString;

        public RepositoryBase(IConfiguration config)
        {
            _connectionString = config.GetConnectionString(ConnectionStringKey);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        protected DbConnection GetOpenConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

    }
}
