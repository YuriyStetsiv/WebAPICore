using Lab3.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services.Interface
{
    public interface IUserCartService
    {
        Task<IEnumerable<UserCartModel>> GetAllUserCart();
        Task<IEnumerable<UserCartModel>> GetAllUserCart(string id);
        Task<UserCartModel> GetUserCartById(int id);
        Task EditUserCart(UserCartModel book);
        Task DeleteUserCart(object id);
        Task AddUserCart(UserCartModel book);
    }
}
