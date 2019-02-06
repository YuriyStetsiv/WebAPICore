using AutoMapper;
using Lab3.Domain.Models.Entities;
using Lab3.Infrastructure.Repositories;
using Lab3.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Infrastructure.Sql.Repositories
{
    class SagesRepository : IRepository<SageModel>
    {
        private readonly DBContext _context;

        public SagesRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SageModel>> GetAll()
        {
            var sageEntities = await _context.Sages.Include(p => p.SagesBooks)
                .ThenInclude(b => b.Book)
                .AsNoTracking()
                .ToListAsync();
            var sageModel = Mapper.Map<IEnumerable<SageModel>>(sageEntities);

            return sageModel;
        }

        public async Task<SageModel> GetById(object id)
        {
            var sageEntities = await _context.Sages.Include(p => p.SagesBooks)
                .ThenInclude(b => b.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == (int)id);
            var sageModel = Mapper.Map<Sages, SageModel>(sageEntities);

            return sageModel;
        }


        public async Task Create(SageModel sageModel)
        {
            var sageEntities = Mapper.Map<SageModel, Sages>(sageModel);

            await _context.Sages.AddAsync(sageEntities);
        }

        public async Task Edit(SageModel sageModel)
        {
            var sageBooks = await _context.Sages.Where(t=>t.Id== sageModel.Id).Select(x=>x.SagesBooks).FirstOrDefaultAsync();

            var sageEntities = Mapper.Map<SageModel, Sages>(sageModel);
            _context.Entry(sageEntities).State = EntityState.Modified;

            if (sageBooks != null)
            {
                _context.SagesBooks.RemoveRange(sageBooks);
                await _context.SaveChangesAsync();
            }

            await _context.SagesBooks.AddRangeAsync(sageEntities.SagesBooks);
        }

        public async Task Delete(object id)
        {
            var sage = await _context.Sages.FindAsync(id);
            if (sage != null)
            {
                IEnumerable<SagesBooks> sagesBooks = await _context.SagesBooks.AsNoTracking().Where(books => books.SageId == (int)id).ToListAsync();
                _context.SagesBooks.RemoveRange(sagesBooks);

                _context.Sages.Remove(sage);
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
