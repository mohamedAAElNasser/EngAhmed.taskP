using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Application.Contracts.IPersistence
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T obj);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateAsync(T obj);
        Task<T> DeleteAsync(T obj);
    }
}
