using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.API.Helpers;
using Users.API.Models;
using Users.API.ResourceParameters;
using Users.API.Services;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UsersController : ControllerBase 
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<UserDto>> GetUsers([FromQuery] UsersResourceParameters usersResourceParameters)
        {
            var usersFromRepo = _userRepository.GetUsers(usersResourceParameters);
            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(Guid userId)
        {
            var userFromRepo = _userRepository.GetUser(userId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(userFromRepo));
        }
    }
}
