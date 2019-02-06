using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using Lab3.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services
{
    public class SageService:ISageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SageModel> GetSageById(int id)
        {
            return await _unitOfWork.SagesRepository.GetById(id);
        }

        public async Task<IEnumerable<SageModel>> GetAllSage()
        {
            return await _unitOfWork.SagesRepository.GetAll();
        }

        public async Task AddSage(SageModel sageModel)
        {
            await _unitOfWork.SagesRepository.Create(sageModel);
            await _unitOfWork.SaveChanges();
        }

        public async Task EditSage(SageModel sageModel)
        {
            await _unitOfWork.SagesRepository.Edit(sageModel);
            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteSage(int id)
        {
            await _unitOfWork.SagesRepository.Delete(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
