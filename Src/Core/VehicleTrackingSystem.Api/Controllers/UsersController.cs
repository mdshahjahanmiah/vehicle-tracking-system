using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.DataObjects.Profiles;
using VehicleTrackingSystem.Domain.Services;
using VehicleTrackingSystem.Validation.Validators;

namespace VehicleTrackingSystem.Api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagement _userManagement;
        private readonly IRequestValidator _requestValidator;
        private readonly ILogger<UsersController> _logger;
        private readonly IErrorMapper _errorMapper;
        public UsersController(IUserManagement userManagement,IRequestValidator requestValidator, ILogger<UsersController> logger, IErrorMapper errorMapper)
        {
            _userManagement = userManagement;
            _requestValidator = requestValidator;
            _logger = logger;
            _errorMapper = errorMapper;
        }

        [HttpPost]
        [Route(ActionsUrls.Login, Name = ActionsUrls.Login)]
        [Consumes(ServiceConsumesType.Json)]
        public IActionResult DoLogin([FromBody]UserViewModel model)
        {
            _logger.LogInformation("Do Login with User : " + model.UserId + " " + model.UserName);
            var user = _userManagement.Authenticate(model.UserName, model.Password);
            if (user == null) return Ok(user);
            return Ok(user);
        }

        [HttpPost]
        [Route(ActionsUrls.Registration, Name = ActionsUrls.Registration)]
        [Consumes(ServiceConsumesType.Json)]
        public IActionResult Register([FromBody]UserViewModel model)
        {
            var response = ServerResponse.OK;
            response = _requestValidator.ValidateUser(model);
            if (response.RespCode != 200) return Ok(response);
            if (model.UserId == 0)
            {
                bool exist = _requestValidator.UserExists(model.UserName);
                if (exist) return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, string.Format(Resource.AlreadyExist, "User")));
            }
            response = _userManagement.AddUser(model);
            return Ok(response);
        }
    }
}