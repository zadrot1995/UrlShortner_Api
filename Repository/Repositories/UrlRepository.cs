using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository.Repositories
{
    public class UrlRepository : IUrlRepository, IDisposable
    {
        private bool disposed = false;

        private readonly ApplicationContext _context;

        public UrlRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public async void Delete(Url url)
        {
            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();

        }

        public IQueryable<Url> Get()
        {
            return _context.Urls.AsQueryable();
        }

        public async Task<Url?> GetById(Guid id)
        {
            return await _context.Urls.FindAsync(id);
        }

        public async Task Insert(Url url)
        {
            await _context.Urls.AddAsync(url);
            await _context.SaveChangesAsync();
        }

        public async void Update(Url url)
        {
            _context.Urls.Update(url);
            await _context.SaveChangesAsync();

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
