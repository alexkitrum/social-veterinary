namespace SocialVeterinary.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPetRepository : ICrudRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetPetsForOwnerAsync(long personId);
    }
}
