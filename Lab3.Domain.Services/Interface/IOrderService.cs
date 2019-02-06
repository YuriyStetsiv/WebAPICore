using Lab3.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services.Interface
{ 
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrderById(int id);
        Task EditOrder(OrderModel order);
        Task DeleteOrder(int id);
        Task AddOrder(OrderModel order);
    }
}
