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

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        public User GetUserById(long id)
        {
            var user = DbSet.SingleOrDefaultAsync<User>(u => u.Id == id)
                            .Result;
            return user;
        }
    }
}
