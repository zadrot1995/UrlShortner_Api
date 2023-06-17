using Domain.Models;
using Infrastructure.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Services
{
    public class UrlServices : IUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public UrlServices(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public bool Delete(Guid id)
        {
            var url = _urlRepository.GetById(id).Result;

            if (url == null)
            {
                //throw new HttpStatusException(HttpStatusCode.NotFound, "Company not found");
            }
            //_urlRepository.Delete(url);
            //_urlRepository.Save();
            return true;
        }

        public IQueryable<Url> Get() => _urlRepository.Get();

        public Url GetById(Guid id)
        {
            var result = _urlRepository.GetById(id).Result;
            if (result != null)
            {
                return result;
            }
            return null;
            //throw new HttpStatusException(HttpStatusCode.NotFound, "Company not found");
        }

        public void Insert(Url url)
        {
            if (url != null)
            {
                using(IUrlRepository urlRepository = _urlRepository)
                {
                    _urlRepository.Insert(url);
                    _urlRepository.Save();
                }
            }
            //throw new HttpStatusException(HttpStatusCode.BadRequest, "Company cannot be null");
        }

        public async void Update(Url url)
        {
            if (url != null)
            {
                _urlRepository.Update(url);
                await _urlRepository.Save();
            }
            //throw new HttpStatusException(HttpStatusCode.BadRequest, "Company cannot be null");
        }
    }
}
