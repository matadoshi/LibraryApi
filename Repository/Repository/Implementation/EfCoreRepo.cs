using DomainModels.Models.Base;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implementation
{
    public class EfCoreRepo<T> : IRepository<T> where T : class,IEntity
    {
        private readonly AppDbContext Db;

        public EfCoreRepo(AppDbContext db)
        {
            Db = db;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await Db.Set<T>().FindAsync(id);
        }

        public async Task<bool> AddAsync(T item)
        {
            try
            {
                await Db.Set<T>().AddAsync(item);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(T item)
        {
            try
            {
                Db.Set<T>().Remove(item);
                await Db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAsync(T item)
        {
            try
            {
                Db.Set<T>().Update(item);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<T>> GetByAuthorAsync(string author)
        {
            return await Db.Set<T>().Where(a=>a.Author == author).ToListAsync();
        }
        public async Task<IList<T>> GetByGenreAsync(string author)
        {
            return await Db.Set<T>().Where(a => a.Genre == author).ToListAsync();
        }
    }
}
