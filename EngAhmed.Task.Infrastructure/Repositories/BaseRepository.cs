using EngAhmed.TaskP.Application.Contracts.IPersistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TaskDbContext _db;

        public BaseRepository(TaskDbContext db)
        {
            _db = db;
        }

        public async Task<T> CreateAsync(T obj)
        {
            await _db.Set<T>().AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<T> DeleteAsync(T obj)
        {
            var keyProperty = typeof(T).GetProperty("Id");
            if (keyProperty == null)
            {
                throw new Exception("Entity does not have an Id property.");
            }

            var idValue = keyProperty.GetValue(obj);
            var local = _db.Set<T>().Local.FirstOrDefault(entry =>
                keyProperty.GetValue(entry)?.Equals(idValue) == true);

            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }

            _db.Set<T>().Remove(obj);
            await _db.SaveChangesAsync();

            return obj;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var _data = await _db.Set<T>().Select(A => A).ToListAsync();
            return _data;
        }

        public async Task<T> UpdateAsync(T obj)
        {
            var keyProperty = typeof(T).GetProperty("Id");
            if (keyProperty == null)
            {
                throw new Exception("Entity does not have an Id property.");
            }

            var idValue = keyProperty.GetValue(obj);
            var local = _db.Set<T>().Local.FirstOrDefault(entry =>
        keyProperty.GetValue(entry)?.Equals(idValue) == true);

            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached; 
            }

            _db.Entry(obj).State = EntityState.Modified; 
            await _db.SaveChangesAsync(); 

            return obj;
        }

    }
}
