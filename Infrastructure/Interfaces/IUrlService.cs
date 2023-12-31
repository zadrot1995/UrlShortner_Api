﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUrlService
    {
        Task<IEnumerable<Url>> Get();
        Url GetById(Guid id);
        void Insert(Url url);
        Task<bool> Delete(Guid id);
        void Update(Url url);
    }
}
