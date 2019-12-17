using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ValidatorValidatorApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string ApiKeyHeaderName = "ApiKey";
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public UserController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();

                _logger.LogInfo($"Returned all users from database.");

                var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);

                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: {id}");

                    var userResult = _mapper.Map<UserDto>(user);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserForCreationDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userEntity = _mapper.Map<User>(user);

                _repository.User.CreateUser(userEntity);
                _repository.Save();

                var createdUser = _mapper.Map<UserDto>(userEntity);

                return CreatedAtRoute("UserById", new { id = createdUser.id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("logout")]
        [HttpPost]
        public IActionResult logout([FromBody]string item)
        {
            try
            {
                Guid token = Guid.Parse(item);
                if (token.GetType() != typeof(Guid))
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                var validationEntity = _repository.Validation.CheckAccessToken(token);

                if (validationEntity == null)
                {
                    _logger.LogError($"validation with access token: {token}, hasn't been found in db.");
                    return NotFound();
                }

                ValidationForUpdateDto val = new ValidationForUpdateDto();

                DateTime dt = DateTime.Now;
                val.access_token = validationEntity.access_token;
                val.user_id = validationEntity.userId;
                val.creation_date = validationEntity.Creation_date;
                val.expiration_date = dt;

                _mapper.Map(val, validationEntity);

                _repository.Validation.updateTokenExpirationTime(validationEntity);
                _repository.Save();
                
                return Ok("logged out");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside logout action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("login")]
        [HttpPost]
        public IActionResult login([FromBody]UserForCreationDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userEntity = _repository.User.GetUserWithDetails(user.username, user.password);

                if (userEntity == null)
                {
                    _logger.LogError("Invalid user credentials sent from client.");
                    return BadRequest("Invalid user credentials");
                }

                var valEntity = _repository.Validation.GetvalidationByUser(userEntity.id);

                if (valEntity == null)
                {

                    Validation val = new Validation();

                    val.userId = userEntity.id;
                    val.Creation_date = DateTime.Now;
                    val.expiration_date = val.Creation_date.AddMinutes(30);

                    _repository.Validation.CreateValidation(val);
                    _repository.Save();

                    var createdVal = _mapper.Map<ValidationDto>(val);

                    createdVal.user = new UserForTransfer();
                    createdVal.user.id = userEntity.id;
                    createdVal.user.username = userEntity.username;

                    return Ok(createdVal);
                }

                valEntity.user = new UserForTransfer();
                valEntity.user.id = userEntity.id;
                valEntity.user.username = userEntity.username;

                return Ok(valEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside login action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{token}")]
        public IActionResult UpdateUser(Guid token, [FromBody]UserForUpdateDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User object sent from client.");
                    return BadRequest("Invalid model object");
                }

                //var validationEntity = _repository.Validation.CheckAccessToken(token);
                //var id = validationEntity.userId;

                var userEntity = _repository.User.GetUserById(token);
                if (userEntity == null)
                {
                    _logger.LogError($"User with id: {token}, hasn't been found in db.");
                    return NotFound();
                }

                user.username = userEntity.username;

                    _mapper.Map(user, userEntity);

                    _repository.User.UpdateUser(userEntity);
                    _repository.Save();
                
                return Ok("password updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);
                if (user == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (user.username == "admin")
                {
                    return Unauthorized();
                }

                var val = _repository.Validation.GetvalidationByUser(user.id);

                DateTime dt = DateTime.Now;
                val.expiration_date = dt;
                
                _repository.Validation.updateTokenExpirationTime(val);

                user.active = false;
                
                _repository.User.DeleteUser(user);
                _repository.Save();

                return Ok("user deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}