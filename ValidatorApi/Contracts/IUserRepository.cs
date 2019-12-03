using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid user_Id);
        User GetUserWithDetails(string username, string password);
        void CreateUser(User user);
    }
}