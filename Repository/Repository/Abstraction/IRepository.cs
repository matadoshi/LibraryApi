using DomainModels.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstraction
{
    public interface IRepository<T> where T :class,IEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetByAuthorAsync(string author);
        Task<IList<T>> GetByGenreAsync(string genre);
        Task<T> GetAsync(int id);
        Task<bool> AddAsync (T item);
        bool UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
    }
}
