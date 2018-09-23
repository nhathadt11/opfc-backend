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

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>User model</returns>
        public User GetUserById(long id)
        {
            try
            {
                var user = _opfcUow.UserRepository.GetUserById(id);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
