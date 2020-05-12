using System.Collections.Generic;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Entities;

namespace VehicleTrackingSystem.Domain.Services
{
    public interface IUserManagement
    {
        UserViewModel Authenticate(string username, string password);
        ServerResponse AddUser(UserViewModel entity);
        void AddUserTypes(UserTypeViewModel entity);
        List<UserTypeViewModel> GetUserTypes();
        List<UserViewModel> GetAllUsers();
        bool UserExists(string userName);
        int GetUserTypeByUser(int userId);
    }
}
