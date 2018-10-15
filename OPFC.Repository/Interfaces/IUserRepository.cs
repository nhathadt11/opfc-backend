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
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User GetUserLogin(string username, string password);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User CreateUser(User user);

        bool IsUserExist(string userName);

        User Update(User user);
    }
}
