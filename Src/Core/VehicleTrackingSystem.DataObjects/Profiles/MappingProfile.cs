using AutoMapper;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Entities;

namespace VehicleTrackingSystem.DataObjects.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserType, UserTypeViewModel>();
            CreateMap<UserTypeViewModel, UserType>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<Vehicle, VehicleViewModel>();
            CreateMap<VehicleViewModel, Vehicle>();
            CreateMap<Device, DeviceViewModel>();
            CreateMap<DeviceViewModel, Device>();
            CreateMap<Location, LocationViewModel>();
            CreateMap<LocationViewModel, Location>();
        }
    }
}
