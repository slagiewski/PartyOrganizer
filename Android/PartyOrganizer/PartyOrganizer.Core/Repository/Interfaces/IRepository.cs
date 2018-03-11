using System.Collections.Generic;

namespace PartyOrganizer.Core.Repository
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();

        T GetByID(int ID);

        void Add(T entity);

        void Remove(T entity);
    }
}