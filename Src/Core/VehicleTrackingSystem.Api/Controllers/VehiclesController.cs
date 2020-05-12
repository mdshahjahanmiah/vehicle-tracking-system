using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.DataObjects.Profiles;
using VehicleTrackingSystem.Domain.Services;
using VehicleTrackingSystem.Validation.Validators;

namespace VehicleTrackingSystem.Api.Controllers
{
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleManagement _vehicleManagement;
        private readonly IRequestValidator _requestValidator;
        private readonly IErrorMapper _errorMapper;
        public VehiclesController(IVehicleManagement vehicleManagement, IRequestValidator requestValidator, IErrorMapper errorMapper)
        {
            _vehicleManagement = vehicleManagement;
            _requestValidator = requestValidator;
            _errorMapper = errorMapper;
        }

        [HttpPost]
        [Route(ActionsUrls.RegisterVehicle, Name = ActionsUrls.RegisterVehicle)]
        [Consumes(ServiceConsumesType.Json)]
        public IActionResult AddVehicle([FromBody]VehicleViewModel model)
        {
            var response = ServerResponse.OK;
            if (string.IsNullOrEmpty(model.Token))  return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, Resource.EmptyToken));
            model.UserId = Convert.ToInt32(_requestValidator.GetUserFromToken(model.Token));
            response = _requestValidator.ValidateVehicle(model);
            if (response.RespCode != 200) return Ok(response);
            if (model.VehicleId == 0)
            {
                bool exist = _requestValidator.DeviceExists(model.Device.ImeiNumber);
                if (exist) return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, string.Format(Resource.AlreadyExist, "Device")));
            }
            else
            {
                if (!_requestValidator.IsPermittedToEditVehicle(model.UserId.ToString(), model.VehicleId)) return Ok(_errorMapper.MapToError(ServerResponse.BadRequest, Resource.NotPermittedToEditVehicle));
            }
            response = _vehicleManagement.AddVehicle(model);
            return Ok(response);
        }

        [HttpGet]
        [Route(ActionsUrls.GetAllVehicles, Name = ActionsUrls.GetAllVehicles)]
        [Consumes(ServiceConsumesType.Json)]
        public List<VehicleViewModel> GetAllVehicles()
        {
            var result = _vehicleManagement.GetAllVehicles();
            return result;
        }
        [HttpGet]
        [Route(ActionsUrls.GetAllVehiclesWithDevices, Name = ActionsUrls.GetAllVehiclesWithDevices)]
        [Consumes(ServiceConsumesType.Json)]
        public List<VehicleViewModel> GetAllVehiclesWithDevices()
        {
            var result = _vehicleManagement.GetAllVehiclesWithDevices();
            return result;
        }
    }
}
