using System.Collections.Generic;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetFriends(User user);
        
    }
}