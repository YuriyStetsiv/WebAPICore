using AutoMapper;
using Lab3.Domain.Models.Entities;
using Lab3.Infrastructure.Repositories;
using Lab3.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Infrastructure.Sql.Repositories
{
    class BooksRepository : IRepository<BookModel>
    {
        private readonly DBContext _context;

        public BooksRepository(DBContext context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<BookModel>> GetAll()
        {
            var bookEntities = await _context.Books.Include(x=>x.SagesBooks)
                .ThenInclude(xa=>xa.Sage)
                .AsNoTracking()
                .ToListAsync();
            var bookModel = Mapper.Map<IEnumerable<BookModel>>(bookEntities);

            return bookModel;
        }

        public async Task<BookModel> GetById(object id)
        {
            var bookEntities = await _context.Books
                .Include(x => x.SagesBooks)
                .ThenInclude(xa => xa.Sage)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == (int)id);
            var bookModel = Mapper.Map<Books, BookModel>(bookEntities);

            return bookModel;
        }

        public async Task Create(BookModel bookModel)
        {
           var bookEntities = Mapper.Map<BookModel, Books>(bookModel); 
            
           await  _context.Books.AddAsync(bookEntities);
        }

        public async Task Edit(BookModel bookModel)
        {
            var sageBooks = await _context.Books.Where(t => t.Id == bookModel.Id).Select(x => x.SagesBooks).FirstOrDefaultAsync();

            var bookEntities = Mapper.Map<BookModel, Books>(bookModel);

           _context.Entry(bookEntities).State = EntityState.Modified;

            if (sageBooks != null)
            {
                _context.SagesBooks.RemoveRange(sageBooks);
                await _context.SaveChangesAsync();
            }

            await _context.SagesBooks.AddRangeAsync(bookEntities.SagesBooks);
        }

        public async Task Delete(object id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                IEnumerable<SagesBooks> sagesBooks = await _context.SagesBooks.AsNoTracking().Where(books => books.BookId == (int)id).ToListAsync();
                IEnumerable<UserCart> userCarts = await _context.UserCart.Where(books => books.BookId == (int)id).ToListAsync();
                IEnumerable<BooksOrders> booksOrders = await _context.BooksOrders.Where(books => books.BookId == (int)id).ToListAsync();

                _context.SagesBooks.RemoveRange(sagesBooks);
                _context.UserCart.RemoveRange(userCarts);
                _context.BooksOrders.RemoveRange(booksOrders);

               _context.Books.Remove(book);
            }
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
