using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Interfaces
{
    /// <summary>
    /// The user service interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">The user identifier</param>
        /// <returns>User model</returns>
        User GetUserById(long id);

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        List<User> GetAllUser();

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User CreateUser(User user);
    }
}
