using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Users.API.Helpers;
using Users.API.Models;
using Users.API.Services;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/usercollections")]

    public class UserCollectionController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserCollectionController(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetUserCollection")]
        public IActionResult GetUserCollection([FromRoute][ModelBinder(binderType: typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var userEntities = _userRepository.GetUsers(ids);

            if (ids.Count() != userEntities.Count())
            {
                return NotFound();
            }

            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);

            return Ok(usersToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<UserDto>> CreateUserCollection(IEnumerable<UserForCreationDto> userCollection)
        {
            var userEntities = _mapper.Map<IEnumerable<Entities.User>>(userCollection);
            foreach (var user in userEntities)
            {
                _userRepository.AddUser(user);
            }

            _userRepository.Save();

            var userCollectionToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);
            var idsAsString = string.Join(",", userCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetUserCollection", new {ids = idsAsString}, userCollectionToReturn);
        }
    }
}
