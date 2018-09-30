using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Implementations
{
    /// <summary>
    /// The user service
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// OPFC Unit Of Work
        /// </summary>
        private readonly IOpfcUow _opfcUow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opfcUow"></param>
        public UserService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public User CreateUser(User user)
        {
            try
            {
                var result = _opfcUow.UserRepository.CreateUser(user);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            try
            {
                var userList = _opfcUow.UserRepository.GetAllUsers();
                return userList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>User model</returns>
        public User GetUserById(long id)
        {
            try
            {
                return _opfcUow.UserRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
