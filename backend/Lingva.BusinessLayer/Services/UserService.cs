using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Context;
using System.Linq;
using Lingva.DataAccessLayer.Exceptions;
using Lingva.DataAccessLayer;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Lingva.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkUser _unitOfWork;

        public UserService(IUnitOfWorkUser unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _unitOfWork.Users.Get(x => x.Username == username);

            if (user == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.Users.GetList();
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.Get(id);
        }

        public User Create(User user, string password)
        {

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new UserServiceException("Password is required");
            }
            if (_unitOfWork.Users.Get(x => x.Username == user.Username) != null)
            {
                throw new UserServiceException("Username \"" + user.Username + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _unitOfWork.Users.Get(userParam.Id);

            if (user == null)
            {
                throw new UserServiceException("User not found");
            }
            if (userParam.Username != user.Username)
            {
                if (_unitOfWork.Users.Get(x => x.Username == userParam.Username) != null)
                    throw new UserServiceException("Username " + userParam.Username + " is already taken");
            }

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _unitOfWork.Users.Update(user);
        }

        public void Delete(int id)
        {
            _unitOfWork.Users.Delete(_unitOfWork.Users.Get(id));
        }

        public string GetUserToken(User user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }
            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            }
            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}