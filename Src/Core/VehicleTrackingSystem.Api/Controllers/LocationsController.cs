using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.DataObjects.Profiles;
using VehicleTrackingSystem.Domain.Services;
using VehicleTrackingSystem.Validation.Validators;

namespace VehicleTrackingSystem.Api.Controllers
{
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationManagement _locationManagement;
        private readonly IRequestValidator _requestValidator;
        private readonly IErrorMapper _errorMapper;
        public LocationsController(ILocationManagement locationManagement, IRequestValidator requestValidator, IErrorMapper errorMapper)
        {
            _locationManagement = locationManagement;
            _requestValidator = requestValidator;
            _errorMapper = errorMapper;
        }

        [HttpPost]
        [Route(ActionsUrls.RecordLocation, Name = ActionsUrls.RecordLocation)]
        [Consumes(ServiceConsumesType.Json)]
        public IActionResult RecordLocation([FromBody]LocationViewModel model)
        {
            var response = ServerResponse.OK;
            if(string.IsNullOrEmpty(model.Token)) return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, Resource.EmptyToken));
            if (!_requestValidator.IsValidToken(model.Token))  return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, Resource.InvalidToken));
            response = _requestValidator.ValidateLocation(model);
            if (response.RespCode != 200) return Ok(response);
            if (!_requestValidator.VehicleExists(model.VehicleId))  return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, string.Format(Resource.NotFound, "Vehicle")));
            var userId = _requestValidator.GetUserFromToken(model.Token);
            if (!_requestValidator.IsPermittedToAddLocation(userId, model.VehicleId))  return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, Resource.NotPermittedToAddPosition));
            response = _locationManagement.RecordLocation(model);
            return Ok(response);
        }

        [HttpPost]
        [Route(ActionsUrls.GetCurrentLocation, Name = ActionsUrls.GetCurrentLocation)]
        [Consumes(ServiceConsumesType.Json)]
        public IActionResult GetCurrentLocation([FromBody]LocationViewModel model)
        {
            var response = ServerResponse.OK;
            if (string.IsNullOrEmpty(model.Token))  return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, Resource.EmptyToken));
            if(!_requestValidator.IsAdministrator(model.Token)) return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, Resource.NotPermittedToInvokeService));
            response.Result = _locationManagement.RetriveLocation(model);
            return Ok(response);
        }

        [HttpGet]
        [Route(ActionsUrls.GetAllLocations, Name = ActionsUrls.GetAllLocations)]
        [Consumes(ServiceConsumesType.Json)]
        public List<LocationViewModel> GetAllLocations()
        {
            var result = _locationManagement.GetAllLocations();
            return result;
        }
    }
}