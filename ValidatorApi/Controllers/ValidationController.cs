using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ValidatorValidatorApi.Controllers
{
    [Route("api/validation")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ValidationController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /*
        [HttpGet("{id}", Name = "validationByUser")]
        public IActionResult GetValidationByUser(Guid id)
        {
            try
            {
                var val = _repository.Validation.GetvalidationByUser(id);

                if (val == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned validation with id: {id}");

                    var userResult = _mapper.Map<ValidationDto>(val);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        //*/

        [HttpPost]
        public IActionResult GetValidationByToken([FromBody]ValidationForCreationDto valDto)
        {
            try
            {
                Guid token = valDto.access_token;
                var val = _repository.Validation.CheckAccessToken(token);

                if (val == null)
                {
                    _logger.LogError($"No valid token with access key: {token} has been found in the db.");
                    return Ok(false);
                }
                else
                {
                    _logger.LogInfo($"Returned validation with access key: {token}");

                    var validationResult = _mapper.Map<ValidationDto>(val);
                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetValidationByToken action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
