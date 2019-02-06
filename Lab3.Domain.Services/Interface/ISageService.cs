using Lab3.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services.Interface
{ 
    public interface ISageService
    {
        Task<IEnumerable<SageModel>> GetAllSage();
        Task<SageModel> GetSageById(int id);
        Task EditSage(SageModel sage);
        Task DeleteSage(int id);
        Task AddSage(SageModel sage);
    }
}
