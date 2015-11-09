using AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Service
{
    public interface IAccountService
    {
        bool ValidateLogin(string userName, string password);
        bool ValidateRegistration(string userName);
        User GetUser(string userName);
        void SaveUser(User user);
        void UpdateAccount(User user);

    }
}