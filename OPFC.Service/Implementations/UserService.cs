﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OPFC.Constants;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public User Authenticate(string username, string password)
        {
            try
            {
                var secret = AppSettings.Secret;

                var user = _opfcUow.UserRepository.GetUserLogin(username, password);

                if (user == null) return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = null;

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User CreateUser(User user)
        {
            try
            {
                var isUserExist =  _opfcUow.UserRepository.IsUserExist(user.Username);

                if (isUserExist) throw new Exception($"{user.Username} is already exist!" );

                user.IsActive = true;
                user.IsDeleted = false;

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
