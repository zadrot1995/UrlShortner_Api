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
    public class UserRepository : IUserRepository, IDisposable
    {
        private bool disposed = false;

        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public async void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public IQueryable<User> Get()
        {
            return _context.Users.AsQueryable();
        }

        public User GetByName(string name)
        {
            return  _context.Users.Where(u => u.Login == name).FirstOrDefault();
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Insert(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async void Update(User user)
        {
            _context.Users.Update(user);
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
