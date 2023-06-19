using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IQueryable<User> Get();
        Task<User?> GetById(Guid id);
        User GetByName(string name);
        Task Insert(User url);
        void Delete(User id);
        void Update(User url);
        Task Save();

    }
}
