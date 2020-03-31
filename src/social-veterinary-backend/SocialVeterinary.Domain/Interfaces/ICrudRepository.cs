
namespace SocialVeterinary.Domain.Interfaces
{
    using System.Threading.Tasks;

    public interface ICrudRepository<T>
    {
        Task<T> GetAsync(long id);
        Task<T> AddAsync(T entity);
    }
}
