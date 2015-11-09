using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models;
using AMS.Repository;
using AMS.Utility;

namespace AMS.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserRepository _userRepository;
        public AccountService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public bool ValidateLogin(string userName, string password)
        {
            var user = _userRepository.GetUser(userName);
            if (user != null)
            {
                return user.Password == Encryption.GetHash(password);
            }
            return false;
        }

        public bool ValidateRegistration(string userName)
        {
            var user = _userRepository.GetUser(userName);
            return user == null;
        }

        public void SaveUser(User user)
        {
            user.Password = Encryption.GetHash(user.Password);
            _userRepository.Add(user);
        }

        public User GetUser(string userName)
        {
            return _userRepository.GetUser(userName);
        }

        public void UpdateAccount(User user)
        {
            user.Password = Encryption.GetHash(user.Password);
            _userRepository.Update(user, _userRepository.GetUserId(user.Email));
        }
    }
}