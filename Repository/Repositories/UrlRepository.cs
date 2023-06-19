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
        }

        public IQueryable<Url> Get()
        {
            return _context.Urls.AsQueryable();
        }

        public async Task<Url?> GetById(Guid id)
        {
            return await _context.Urls.Include(url => url.Creator).Where(url => url.Id == id).FirstOrDefaultAsync();
        }

        public void Insert(Url url)
        {
            _context.Urls.Add(url);
        }

        public void Update(Url url)
        {
            _context.Urls.Update(url);

        }

        public void Save()
        {
            _context.SaveChanges();
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
