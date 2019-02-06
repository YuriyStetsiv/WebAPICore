using Lab3.Domain.Models.Entities;
using Lab3.Infrastructure.Repositories;
using Lab3.Infrastructure.Sql.Repositories;
using Lab3.Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Lab3.Infrastructure.Sql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _db;
        private bool _disposed;

        private IRepository<SageModel> _sageRepository;
        private IRepository<BookModel> _bookRepository;
        private IRepository<OrderModel> _orderRepository;
        private IUserCartRepository _userCartRepository;

        public UnitOfWork(DBContext context)
        {
            _db = context;
            AutoMapperConfig.Configure();
        }

        public IRepository<SageModel> SagesRepository => _sageRepository ??
                                                              (_sageRepository =
                                                                  new SagesRepository(_db));

        public IRepository<BookModel> BooksRepository => _bookRepository ??
                                                              (_bookRepository =
                                                                    new BooksRepository(_db));

        public IRepository<OrderModel> OrdersRepository => _orderRepository ??
                                                               (_orderRepository =
                                                                   new OrdersRepository(_db));
          
        public IUserCartRepository UserCartRepository => _userCartRepository ??
                                                               (_userCartRepository =
                                                                   new UserCartRepository(_db));       

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
           await _db.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _db.Dispose();
                _disposed = true;
            }
        }
    }
}
