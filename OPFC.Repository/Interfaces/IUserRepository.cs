using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    /// <summary>
    /// The User Repository Interface
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List user model</returns>
        List<User> GetAllUsers();

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User CreateUser(User user);
    }
}
