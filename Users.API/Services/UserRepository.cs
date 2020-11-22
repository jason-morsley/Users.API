using Users.API.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using Users.API.DbContexts;

namespace Users.API.Services
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // the repository automatically assigns an Id.
            user.Id = Guid.NewGuid();

            _context.Users.Add(user);
        }

        public bool UserExists(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _context.Users.Any(a => a.Id == userId);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
        }
        
        public User GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _context.Users.FirstOrDefault(a => a.Id == userId);
        }

        public IEnumerable<User> GetUsers(string location, string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(location)
                && string.IsNullOrWhiteSpace(searchQuery))
            {
                return GetUsers();

            }

            var collection = _context.Users as IQueryable<User>;

            if (!string.IsNullOrWhiteSpace(location))
            {
                location = location.Trim();
                collection = collection.Where(a => a.Location == location);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Location.Contains(searchQuery)
                                                   || a.FirstName.Contains(searchQuery)
                                                   || a.LastName.Contains(searchQuery));
            }

            return collection.ToList();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList<User>();
        }
         
        public IEnumerable<User> GetUsers(IEnumerable<Guid> userIds)
        {
            if (userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
            }

            return _context.Users.Where(a => userIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public void UpdateUser(User user)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
               // dispose resources when needed
            }
        }
    }
}
