using Lab3.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Infrastructure.Repositories
{
    public interface IUserCartRepository:IRepository<UserCartModel>
    {
        Task<IEnumerable<UserCartModel>> GetAll(string id);
    }
}
