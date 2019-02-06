using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using Lab3.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderModel> GetOrderById(int id)
        {
            return await _unitOfWork.OrdersRepository.GetById(id);
        }

        public async Task < IEnumerable<OrderModel>> GetAllOrders()
        {
            return await _unitOfWork.OrdersRepository.GetAll();
        }

        public async Task AddOrder(OrderModel orderModel)
        {
            await _unitOfWork.OrdersRepository.Create(orderModel);
            await _unitOfWork.SaveChanges();
        }

        public async Task EditOrder(OrderModel orderModel)
        {
            await _unitOfWork.OrdersRepository.Edit(orderModel);
            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteOrder(int id)
        {
            await _unitOfWork.OrdersRepository.Delete(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
