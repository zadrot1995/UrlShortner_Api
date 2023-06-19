using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Repositories;
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

        public async Task<bool> Delete(Guid id)
        {
            var url = _urlRepository.GetById(id).Result;

            if (url == null)
            {
                //throw new HttpStatusException(HttpStatusCode.NotFound, "Company not found");
            }
            using (IUrlRepository urlRepository = _urlRepository)
            {
                urlRepository.Delete(url);
                urlRepository.Save();
            }
            return true;
        }

        public async Task<IEnumerable<Url>> Get()
        {
            using (IUrlRepository urlRepository = _urlRepository)
            {
                var result = await urlRepository.Get().Include(url => url.Creator).ToListAsync();
                return result;
            }
            return null;
        }

        public Url GetById(Guid id)
        {
            using (IUrlRepository urlRepository = _urlRepository)
            {
                var result = urlRepository.GetById(id).Result;
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            
            return null;
        }

        public void Insert(Url url)
        {

            if (url != null)
            {
                using(IUrlRepository urlRepository = _urlRepository)
                {
                    var isAlreadyExist = urlRepository.Get().Where(u => url.LongUrl == u.LongUrl).Any();
                    if (!isAlreadyExist)
                    {
                        url.ShortUrl = url.LongUrl.Substring(0,8) + DateTime.UtcNow.Ticks.ToString("x");
                        urlRepository.Insert(url);
                        urlRepository.Save();
                    }
                    else throw new Exception("Already Exist");
                }
            }
            //throw new HttpStatusException(HttpStatusCode.BadRequest, "Company cannot be null");
        }

        public async void Update(Url url)
        {
            if (url != null)
            {
                using (IUrlRepository urlRepository = _urlRepository)
                {
                    urlRepository.Update(url);
                    urlRepository.Save();
                }
            }
            //throw new HttpStatusException(HttpStatusCode.BadRequest, "Company cannot be null");
        }
    }
}
