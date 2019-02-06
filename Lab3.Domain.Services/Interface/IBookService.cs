using Lab3.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Domain.Services.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task EditBook(BookModel book);
        Task DeleteBook(int id);
        Task AddBook(BookModel book);
    }
}
