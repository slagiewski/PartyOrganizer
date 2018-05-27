using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IUserRepositoryAsync : IRepositoryAsync<User>
    {
        Task<IEnumerable<User>> GetFriends(User user);
    }
}