using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetById(Guid id);
        User GetByName(string name);
        void Insert(User url);
        Task<bool> Delete(Guid id);
        void Update(User url);
    }
}
