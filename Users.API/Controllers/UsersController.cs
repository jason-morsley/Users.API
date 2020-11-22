using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.API.Helpers;
using Users.API.Models;
using Users.API.Services;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UsersController : ControllerBase 
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet()]
        public IActionResult GetUsers()
        {
            var usersFromRepo = _userRepository.GetUsers();
            var users = new List<UserDto>();

            foreach (var user in usersFromRepo)
            {
                users.Add(new UserDto()
                {
                    Id = user.Id,
                    Name = $"{user.FirstName} {user.LastName}",
                    Age = user.DateOfBirth.GetAgeFromDob()
                });
            }

            return Ok(users);
        }

        [HttpGet("{userId:guid}")]
        public IActionResult GetUser(Guid userId)
        {
            var userFromRepo = _userRepository.GetUser(userId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            return Ok(userFromRepo);
        }
    }
}
