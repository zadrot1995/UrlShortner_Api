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
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = _userRepository.GetById(id).Result;

            if (user == null)
            {
                //throw new HttpStatusException(HttpStatusCode.NotFound, "Company not found");
            }
            using (IUserRepository userRepository = _userRepository)
            {
                userRepository.Delete(user);
                await userRepository.Save();
            }
            return true;
        }
        public async Task<IEnumerable<User>> Get() 
        {
            using (IUserRepository userRepository = _userRepository)
            {
                var result = await userRepository.Get().ToListAsync();
                return result;
            }
            return null;
        }

        public User GetByName(string name)
        {
            using (IUserRepository userRepository = _userRepository)
            {
                var result = userRepository.GetByName(name);
                if (result != null)
                {
                    return result;
                }
                return null;
            }

            return null;
        }

        public async Task<User> GetById(Guid id)
        {
            using (IUserRepository userRepository = _userRepository)
            {
                var result = await userRepository.GetById(id);
                if (result != null)
                {
                    return result;
                }
                return null;
            }

            return null;

        }

        public void Insert(User user)
        {
            if (user != null)
            {
                using (IUserRepository userRepository = _userRepository)
                {
                    userRepository.Insert(user);
                    userRepository.Save();
                }
            }
        }

        public async void Update(User user)
        {
            if (user != null)
            {
                using (IUserRepository userRepository = _userRepository)
                {
                    userRepository.Update(user);
                    userRepository.Save();
                }
            }
        }
    }
}
