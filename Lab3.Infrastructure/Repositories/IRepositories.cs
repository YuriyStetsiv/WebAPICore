using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Infrastructure.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        Task Create(T item);
        Task Edit(T item);
        Task Delete(object id);
    }
}
