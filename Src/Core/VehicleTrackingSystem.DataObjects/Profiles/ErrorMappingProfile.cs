using System;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;

namespace VehicleTrackingSystem.DataObjects.Profiles
{
    public class ErrorMappingProfile : IErrorMapper
    {
        public ServerResponse MapToError(ServerResponse response,string errorDetail)
        {
            ServerResponse errorResponse = response;
            errorResponse.RespDesc = errorDetail;
            return errorResponse;
            
        }

        public UserViewModel MapToError(UserViewModel model, ServerResponse response, string errorDetail)
        {
            UserViewModel request = new UserViewModel
            {
                RespCode = 400,
                RespDesc = errorDetail
            };
            return request;
        }
    }
}
