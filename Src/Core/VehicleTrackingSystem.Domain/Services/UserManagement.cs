using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VehicleTrackingSystem.DataAccess.Repository;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.DataObjects.Profiles;
using VehicleTrackingSystem.Entities;
using VehicleTrackingSystem.Security.Handlers;

namespace VehicleTrackingSystem.Domain.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserType> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly ICryptographyHandler _cryptographyHandler;
        private readonly IErrorMapper _errorMapper;
        public UserManagement(IUnitOfWork unitOfWork, IRepository<UserType> repository, IRepository<User> userRepository,IMapper mapper, IJwtTokenHandler jwtTokenHandler, ICryptographyHandler cryptographyHandler, IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenHandler = jwtTokenHandler;
            _cryptographyHandler = cryptographyHandler;
            _errorMapper = errorMapper;
        }
        public void AddUserTypes(UserTypeViewModel entity)
        {
            var model = _mapper.Map<UserType>(entity);
            _repository.Add(model);
            _unitOfWork.Commit();

        }
        public UserViewModel Authenticate(string username, string password)
        {
            var response = ServerResponse.OK;
            var result = _userRepository.Get().SingleOrDefault(x => x.UserName == username);
            if (result == null) return _errorMapper.MapToError(null,ServerResponse.BadRequest, "User is not found.");
            bool isValid = _cryptographyHandler.VerifyGeneratedHash(password, result.Password);
            if (!isValid) return _errorMapper.MapToError(null,ServerResponse.BadRequest, "Username or password is incorrect.");
            var user = _mapper.Map<UserViewModel>(result);
            user.Token = _jwtTokenHandler.GenerateJwtSecurityToken(user.UserId.ToString());
            user.Password = null;
            return user;
        }
        public List<UserTypeViewModel> GetUserTypes()
        {
            var result = _repository.Get();
            var list = _mapper.Map<List<UserTypeViewModel>>(result);
            return list;
        }
        public List<UserViewModel> GetAllUsers()
        {
            var result = _userRepository.Get();
            var list = _mapper.Map<List<UserViewModel>>(result);
            return list;
        }
        public bool UserExists(string userName)
        {
            return _userRepository.Get().Any(e => e.UserName == userName);
        }
        public ServerResponse AddUser(UserViewModel entity)
        {
            var response = ServerResponse.OK;
            if(entity.UserId == 0) entity.Password = _cryptographyHandler.GeneratePasswordHash(entity.Password);
            var model = _mapper.Map<User>(entity);
            if (entity.UserId == 0) _userRepository.Add(model);
            else response = Update(entity, response, model);
            if(response.RespCode == 200) _unitOfWork.Commit();
            return response;
        }
        private ServerResponse Update(UserViewModel entity, ServerResponse response, User model)
        {
            var user = _userRepository.Get().SingleOrDefault(e => e.UserId == entity.UserId);
            if (user == null)
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "User is not found";
                return response;
            }
            model.UserName = user.UserName;
            model.Password = user.Password;
            _userRepository.Update(model);
            return response;
        }
        public int GetUserTypeByUser(int userId)
        {
            return _userRepository.Get().FirstOrDefault(u => u.UserId == userId).UserTypeId;
        }
    }
}
