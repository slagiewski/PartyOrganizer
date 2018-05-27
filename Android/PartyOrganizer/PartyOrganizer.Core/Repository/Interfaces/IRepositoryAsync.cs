using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyOrganizer.Core.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(string ID);

        Task<int> Add(T entity);

        Task Remove(T entity);
    }
}