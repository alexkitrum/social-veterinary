namespace SocialVeterinary.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPersonRepository : ICrudRepository<Person>
    {
        Task<IEnumerable<Person>> GetAsync();
    }
}
