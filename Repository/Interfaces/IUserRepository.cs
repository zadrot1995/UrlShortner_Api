using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IQueryable<User> Get();
        Task<User> GetById(Guid id);
        Task Insert(User user);
        Task Delete(Guid id);
        Task Update(User user);
        Task Save();
    }
}
