using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository.Interfaces
{
    public interface IUrlRepository : IDisposable
    {
        IQueryable<Url> Get();
        Task<Url?> GetById(Guid id);
        Task Insert(Url url);
        void Delete(Url id);
        void Update(Url url);
        Task Save();

    }
}
