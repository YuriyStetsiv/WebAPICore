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
    class OrdersRepository : IRepository<OrderModel>
    {
        private readonly DBContext _context;

        public OrdersRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderModel>> GetAll()
        {
            var orderEntities = await _context.Orders
                .Include(o => o.BooksOrders)
                .ThenInclude(b => b.Book)
                .AsNoTracking()
                .ToListAsync();
            var orderModel = Mapper.Map<IEnumerable<OrderModel>>(orderEntities);

            return orderModel;
        }

        public async Task<OrderModel> GetById(object id)
        {
            var orderEntities = await _context.Orders
                .Include(o=>o.BooksOrders)
                .ThenInclude(b=>b.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == (int)id);
            var orderModel = Mapper.Map<Orders, OrderModel>(orderEntities);

            return orderModel;
        }

        public async Task Create(OrderModel orderModel)
        {
            var orderEntities = Mapper.Map<OrderModel, Orders>(orderModel);

            await _context.Orders.AddAsync(orderEntities);
        }

        public async Task Edit(OrderModel orderModel)
        {
            var orderBooks = await _context.Orders.Where(t => t.Id == orderModel.Id).Select(x => x.BooksOrders).FirstOrDefaultAsync();

            var orderEntity = Mapper.Map<OrderModel, Orders>(orderModel);
            _context.Entry(orderEntity).State = EntityState.Modified;

            if (orderBooks != null)
            {
                _context.BooksOrders.RemoveRange(orderBooks);
                await _context.SaveChangesAsync();
            }

            await _context.BooksOrders.AddRangeAsync(orderEntity.BooksOrders);
        }

        public async Task Delete(object id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
                _context.Orders.Remove(order);
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
