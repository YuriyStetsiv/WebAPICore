using System;
using System.Threading.Tasks;
using Lab3.Domain.Models.Entities;
using Lab3.Infrastructure.Repositories;

namespace Lab3.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        Task SaveChanges();
        IRepository<SageModel> SagesRepository { get; }
        IRepository<BookModel> BooksRepository { get; }
        IRepository<OrderModel> OrdersRepository { get; }
        IUserCartRepository UserCartRepository { get; }      
    }
}

