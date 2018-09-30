using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext): base(dbContext) { }

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
    }
}
