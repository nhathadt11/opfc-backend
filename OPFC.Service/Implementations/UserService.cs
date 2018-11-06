using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OPFC.Constants;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                Expires = DateTime.UtcNow.AddHours(1 * 24 * 10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public User CreateUser(User user)
        {
            try
            {
                var isUserExist = _opfcUow.UserRepository.IsUserExist(user.Username);

                if (isUserExist) throw new Exception($"{user.Username} is already exist!");

                user.IsActive = true;
                user.IsDeleted = false;

                var result = _opfcUow.UserRepository.CreateUser(user);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User Update(User user)
        {
            try
            {
                var result = _opfcUow.UserRepository.Update(user);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsUserExist(string userName)
        {
            try
            {
                return _opfcUow.UserRepository.IsUserExist(userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCityNameForUserId(long userId)
        {
            var cityId = _opfcUow.UserRepository
                .GetAllUsers()
                .Find(u => u.Id == userId)
                .CityId;

            return _opfcUow.CityRepository.GetCityById(cityId).Name;
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            var userList = _opfcUow.UserRepository.GetAllUsers();
            return userList;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>User model</returns>
        public User GetUserById(long id)
        {
            return _opfcUow.UserRepository.GetById(id);
        }

        public User GetUserByBrandId(long brandId)
        {
            Brand foundBrand = _opfcUow.BrandRepository.GetBrandById(brandId);
            if (foundBrand == null)
            {
                throw new Exception("Brand could not be found.");
            }

            return _opfcUow.UserRepository.GetById(foundBrand.UserId);
        }

        public User GetUserWhoMadeOrderLineId(long orderLineId)
        {
            var foundOrderLine = _opfcUow.OrderLineRepository.GetById(orderLineId);
            if (foundOrderLine == null)
            {
                throw new Exception("OrderLine could not be found.");
            }

            var foundOrder = _opfcUow.OrderRepository
                .GetAll()
                .SingleOrDefault(o => o.OrderId == foundOrderLine.OrderId);
            if (foundOrderLine == null)
            {
                throw new Exception("Order could not be found.");
            }

            var eventPlannerUser = _opfcUow.UserRepository.GetById(foundOrder.UserId);
            if (eventPlannerUser == null)
            {
                throw new Exception("User could not be found");
            }

            return eventPlannerUser;
        }
    }
}
