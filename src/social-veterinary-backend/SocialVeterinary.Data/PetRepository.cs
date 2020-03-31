namespace SocialVeterinary.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dapper;

    using Microsoft.Extensions.Configuration;

    using SocialVeterinary.Domain;
    using SocialVeterinary.Domain.Interfaces;

    public class PetRepository : RepositoryBase, IPetRepository
    {
        public PetRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<Pet> GetAsync(long id)
        {
            await using var connection = GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<Pet>(
                       "SELECT * FROM pets " +
                       "WHERE id=@id",
                       new
                           {
                               id
                           });
        }

        public async Task<IEnumerable<Pet>> GetPetsForOwnerAsync(long personId)
        {
            await using var connection = GetOpenConnection();
            return await connection.QueryAsync<Pet>(
                       "SELECT * FROM pets " +
                       "WHERE person_id=@personId",
                       new
                           {
                               personId
                           });
        }

        public async Task<Pet> AddAsync(Pet entity)
        {
            await using (var connection = GetOpenConnection())
            {
                var id = await connection.ExecuteScalarAsync<long>(
                             "INSERT INTO pets(name, type, person_id) " +
                             "VALUES(@name, @type, @personId);" +
                             "SELECT LAST_INSERT_ID();",
                             new
                                 {
                                     name = entity.Name,
                                     type = entity.Type,
                                     personId = entity.OwnerId
                                 });
                entity.Id = id;
            }

            return entity;
        }
    }
}
