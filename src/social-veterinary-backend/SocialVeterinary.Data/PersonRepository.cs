namespace SocialVeterinary.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dapper;

    using Microsoft.Extensions.Configuration;

    using SocialVeterinary.Domain;
    using SocialVeterinary.Domain.Interfaces;

    public class PersonRepository : RepositoryBase, IPersonRepository
    {
        public PersonRepository(IConfiguration config) : base(config)
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
                             "INSERT INTO persons(name, last_name, is_employee ) " +
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

        public async Task<IEnumerable<Person>> Get()
        {
            await using var connection = GetOpenConnection();
            return await connection.QueryAsync<Person>("SELECT * FROM persons");
        }
    }
}
