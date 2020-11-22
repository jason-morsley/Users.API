using Users.API.Entities;
using System;
using System.Collections.Generic;
using Users.API.ResourceParameters;

namespace Users.API.Services
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(UsersResourceParameters usersResourceParameters);
        User GetUser(Guid userId);
        IEnumerable<User> GetUsers(IEnumerable<Guid> userIds);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        bool UserExists(Guid userId);
        bool Save();
    }
}
