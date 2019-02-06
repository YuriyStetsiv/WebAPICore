using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using Lab3.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services
{
    public class UserCartService:IUserCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserCartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserCartModel> GetUserCartById(int id)
        {
            return await _unitOfWork.UserCartRepository.GetById(id);
        }

        public async Task<IEnumerable<UserCartModel>> GetAllUserCart()
        {
            return await _unitOfWork.UserCartRepository.GetAll();
        }

        public async Task<IEnumerable<UserCartModel>> GetAllUserCart(string id)
        {
            return await _unitOfWork.UserCartRepository.GetAll(id);
        }

        public async Task AddUserCart(UserCartModel sageCartModel)
        {
            await _unitOfWork.UserCartRepository.Create(sageCartModel);
            await _unitOfWork.SaveChanges();
        }

        public async Task EditUserCart(UserCartModel sageCartModel)
        {
            await _unitOfWork.UserCartRepository.Edit(sageCartModel);
            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteUserCart(object id)
        {
            await _unitOfWork.UserCartRepository.Delete(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
