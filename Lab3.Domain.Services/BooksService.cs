using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using Lab3.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services
{
    public class BooksService :IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _unitOfWork.BooksRepository.GetById(id);
        }

        public async Task<IEnumerable<BookModel>> GetAllBooks()
        {
            return await _unitOfWork.BooksRepository.GetAll();
        }

        public async Task AddBook(BookModel bookModel)
        {
           await _unitOfWork.BooksRepository.Create(bookModel);
           await _unitOfWork.SaveChanges();
        }

        public async Task EditBook(BookModel bookModel)
        {
           await _unitOfWork.BooksRepository.Edit(bookModel);
           await  _unitOfWork.SaveChanges();
        }

        public async Task DeleteBook(int id)
        {
           await _unitOfWork.BooksRepository.Delete(id);
           await _unitOfWork.SaveChanges();
        }
    }
}
