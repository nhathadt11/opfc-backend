using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext): base(dbContext) { }

        public User GetUserLogin(string username, string password)
        {
            return DbSet.SingleOrDefault(u => u.Username == username && u.Password == password
                                           && u.IsActive == true && u.IsDeleted == false);
        }

        public User CreateUser(User user)
        {
            return DbSet.Add(user).Entity;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            var userList = DbSet.Include(u => u.UserRole)
                                .Include(u => u.EventAddressList)
                                .Include(u => u.BrandList)
                                .ToListAsync<User>()
                                .Result;
            return userList;
        }

        public bool IsUserExist(string userName)
        {
            return DbSet.Any(u => u.Username == userName);
        }
    }
}
