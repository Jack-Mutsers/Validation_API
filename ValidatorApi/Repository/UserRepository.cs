using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(us => us.username)
                .ToList();
        }

        public User GetUserById(Guid userId)
        {
            return FindByCondition(user => user.id.Equals(userId))
                    .FirstOrDefault();
        }

        public User GetUserWithDetails(string username, string password)
        {
            return FindByCondition(user => user.username.Equals(username) && user.password.Equals(password))
                .FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            Create(user);
        }
    }
}
