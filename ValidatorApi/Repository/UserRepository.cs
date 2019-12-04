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
                .Where(us => us.active.Equals(true))
                .ToList();
        }

        public User GetUserById(Guid userId)
        {
            return FindByCondition(user => user.id.Equals(userId))
                    .FirstOrDefault();
        }

        public User GetUserWithDetails(string username, string password)
        {
            return FindByCondition(user => user.username.Equals(username) && user.password.Equals(password) && user.active.Equals(true))
                .FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }

        public void DeleteUser(User user)
        {
            Update(user);
        }
    }
}
