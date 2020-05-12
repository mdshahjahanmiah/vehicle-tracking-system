using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Domain.Services;
using VehicleTrackingSystem.Security.Handlers;
using VehicleTrackingSystem.Validation.Enums;

namespace VehicleTrackingSystem.Validation.Validators
{
    public class RequestValidator : IRequestValidator
    {
        private readonly IDeviceManagement _deviceManagement;
        private readonly IUserManagement _userManagement;
        private readonly IVehicleManagement _vehicleManagement;
        private readonly IJwtTokenHandler _tokenHandler;
        public RequestValidator(IDeviceManagement deviceManagement, IUserManagement userManagement, IVehicleManagement vehicleManagement, IJwtTokenHandler tokenHandler)
        {
            _deviceManagement = deviceManagement;
            _userManagement = userManagement;
            _vehicleManagement = vehicleManagement;
            _tokenHandler = tokenHandler;
        }
        public bool DeviceExists(Guid number)
        {
            return _deviceManagement.DeviceExists(number);
        }
        public bool UserExists(string userName)
        {
            return _userManagement.UserExists(userName);
        }
        public bool VehicleExists(int vehicleId)
        {
            return _vehicleManagement.IsVehicleExists(vehicleId);
        }
        public bool IsAdministrator(string token)
        {
            var result = _tokenHandler.VerifyJwtSecurityToken(token);
            if (string.IsNullOrEmpty(result)) return false;
            int userTypeId = _userManagement.GetUserTypeByUser(Convert.ToInt32(result));
            if (userTypeId == UserTypes.Admin) return true;
            return false;
        }
        public bool IsValidToken(string token)
        {
            var result = _tokenHandler.VerifyJwtSecurityToken(token);
            if (string.IsNullOrEmpty(result)) return false;
            return true;
        }
        public string GetUserFromToken(string token)
        {
            var result = _tokenHandler.VerifyJwtSecurityToken(token);
            return result;
        }
        public bool IsPermittedToAddLocation(string userId, int vehicleId)
        {
            var user = _vehicleManagement.GetUserByVehicle(vehicleId);
            if (user == null) return false;
            if (userId == user.UserId.ToString()) return true;
            return false;
        }
        public bool IsPermittedToEditVehicle(string userId, int vehicleId)
        {
            var user = _vehicleManagement.GetUserByVehicle(vehicleId);
            if (user == null) return false;
            if (userId == user.UserId.ToString()) return true;
            return false;
        }

        public ServerResponse ValidateLocation(LocationViewModel request)
        {
            var response = ServerResponse.OK;
            if (request.VehicleId == 0)
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(VehicleId).";
            }
            else if (request.Latitude == 0)
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Latitude).";
            }
            else if (request.Longitude == 0)
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Longitude).";
            }
            return response;
        }
        public ServerResponse ValidateVehicle(VehicleViewModel request)
        {
            var response = ServerResponse.OK;
            if (string.IsNullOrEmpty(request.Name))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Name)";
            }
            else if (string.IsNullOrEmpty(request.Model))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Model).";
            }
            else if (string.IsNullOrEmpty(request.Year))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Year).";
            }
            else if (string.IsNullOrEmpty(request.Maker))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Maker).";
            }
            return response;
        }
        public ServerResponse ValidateUser(UserViewModel request)
        {
            var response = ServerResponse.OK;
            if (request.UserTypeId == 0)
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(User Type)";
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Name)";
            }
            else if (string.IsNullOrEmpty(request.Email))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Email).";
            }
            else if (string.IsNullOrEmpty(request.MobileNumber))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(MobileNumber).";
            }
            else if (string.IsNullOrEmpty(request.UserName))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(UserName).";
            }
            else if (string.IsNullOrEmpty(request.Password))
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Required field is missing(Password).";
            }
            return response;
        }
    }
}
