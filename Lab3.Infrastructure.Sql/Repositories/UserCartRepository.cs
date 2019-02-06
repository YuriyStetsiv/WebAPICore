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
    public class UserCartRepository :IUserCartRepository
    {
        private readonly DBContext _context;

        public UserCartRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserCartModel>> GetAll()
        {
            var sageCartEntities = await _context.UserCart.Include(book => book.Book)
                .AsNoTracking()
                .ToListAsync();
            var sageCartModel = Mapper.Map<IEnumerable<UserCartModel>>(sageCartEntities);

            return sageCartModel;
        }
        public async Task<IEnumerable<UserCartModel>> GetAll(string id)
        {
            var sageCartEntities = await _context.UserCart
                .Include(book => book.Book)
                .ThenInclude(sageBook=>sageBook.SagesBooks)
                .ThenInclude(sage=>sage.Sage)
                .AsNoTracking()
                .Where(userid=>userid.UserId==id).ToListAsync();

            var  sageCartModel =  Mapper.Map<IEnumerable<UserCartModel>>(sageCartEntities);

            return  sageCartModel;
        }

        public async Task<UserCartModel> GetById(object id)
        {
            var sageCartEntities = await _context.UserCart
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == (string)id);

            var sageCartModel = Mapper.Map<UserCart, UserCartModel>(sageCartEntities);

            return sageCartModel;
        }

        public async Task Create(UserCartModel sageCartModel)
        {
            var sageCartEntities = Mapper.Map<UserCartModel, UserCart>(sageCartModel);

            await _context.UserCart.AddAsync(sageCartEntities);
        }

        public async Task Edit(UserCartModel bookCartModel)
        {
            var sageCartEntities = Mapper.Map<UserCartModel, UserCart>(bookCartModel);
            _context.Entry(sageCartEntities).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            var userCharModel = (UserCartModel)id; 

            var userChartEntities = Mapper.Map<UserCartModel, UserCart>(userCharModel);
            
            var userChart = await _context.UserCart.SingleOrDefaultAsync(u => u.BookId == userChartEntities.BookId && u.UserId == userChartEntities.UserId);

            if (userChart != null)
                _context.UserCart.Remove(userChart);
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
