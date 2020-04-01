using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

using SocialVeterinary.Domain;

namespace SocialVeterinary.Data.IntegrationTests.DbHelpers
{
    public class PersonDbHelper
    {
        public PersonDbHelper()
        {
            
        }
        public async Task<Person> GetAsync(long id)
        {
            await using var connection = GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<Person>(
                       "SELECT * FROM persons " +
                       "WHERE id=@id",
                       new
                       {
                           id
                       });
        }

        public async Task<Person> AddAsync(Person entity)
        {
            await using (var connection = GetOpenConnection())
            {
                var id = await connection.ExecuteScalarAsync<long>(
                             "INSERT INTO persons(name, last_name, is_employee) " +
                             "VALUES(@name, @lastName, @isEmployee);" +
                             "SELECT LAST_INSERT_ID();",
                             new
                             {
                                 name = entity.Name,
                                 lastName = entity.LastName,
                                 isEmployee = entity.IsEmployee
                             });
                entity.Id = id;
            }

            return entity;
        }

        public async Task<IEnumerable<Person>> GetAsync()
        {
            await using var connection = GetOpenConnection();
            return await connection.QueryAsync<Person>(@"SELECT p.*, COUNT(pt.id) as PetsCount FROM persons p
                                    LEFT JOIN pets pt ON pt.person_id = p.id 
                                    GROUP BY p.id");
        }

        public async Task DeleteAsync(long[] ids)
        {
            await using var connection = GetOpenConnection();
            await connection.ExecuteAsync(@"DELETE FROM persons
                            WHERE id IN (@ids)",
                       new
                           {
                               ids
                           });
        }

        protected DbConnection GetOpenConnection()
        {
            // Note: connection string is hardcoded to keep it simple.
            var connection = new MySqlConnection("server=localhost;uid=user;pwd=admin123!;database=db");
            connection.Open();
            return connection;
        }
    }
}
