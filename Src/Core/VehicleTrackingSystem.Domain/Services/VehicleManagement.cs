using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleTrackingSystem.DataAccess.Repository;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Entities;

namespace VehicleTrackingSystem.Domain.Services
{
    public class VehicleManagement : IVehicleManagement
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Vehicle> _repository;
        private readonly IRepository<Device> _deviceRepository;
        private readonly IMapper _mapper;
        public VehicleManagement(IUnitOfWork unitOfWork, IRepository<Vehicle> repository, IRepository<Device> deviceRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
            _deviceRepository = deviceRepository;
        }
        public ServerResponse AddVehicle(VehicleViewModel entity)
        {
            var response = ServerResponse.OK;
            var model = _mapper.Map<Vehicle>(entity);
            if (entity.VehicleId == 0) _repository.Add(model);
            else response = Update(entity, response, model);
            if (response.RespCode == 200) _unitOfWork.Commit();
            return response;

        }
        public List<VehicleViewModel> GetAllVehicles()
        {
            var result = _repository.Get();
            var list = _mapper.Map<List<VehicleViewModel>>(result);
            return list;
        }
        public List<VehicleViewModel> GetAllVehiclesWithDevices()
        {
            var result = (from m in _repository.Get()
                         join d in _deviceRepository.Get() on m.DeviceId equals d.DeviceId
                         select new Vehicle
                         {
                             VehicleId = m.VehicleId,
                             Name = m.Name,
                             Maker = m.Maker,
                             Model = m.Model,
                             UserId = m.UserId,
                             Year = m.Year,
                             BodyType = m.BodyType,
                             DeviceId = m.DeviceId,
                             Device = new Device
                             {
                                 DeviceId = d.DeviceId,
                                 ImeiNumber = d.ImeiNumber,
                                 Name = d.Name,
                                 Status = d.Status
                             }
                         }).ToList();
            var list = _mapper.Map<List<VehicleViewModel>>(result);
            return list;
        }
        public VehicleViewModel GetUserByVehicle(int vehicleId)
        {
            var result = _repository.Get().Where(t => t.VehicleId == vehicleId).FirstOrDefault();
            var vechile = _mapper.Map<VehicleViewModel>(result);
            return vechile;
        }
        public bool IsVehicleExists(int vehicleId)
        {
            return _repository.Get().Any(e => e.VehicleId == vehicleId);
        }
        private ServerResponse Update(VehicleViewModel entity, ServerResponse response, Vehicle model)
        {
            var vehicle = _repository.Get().SingleOrDefault(e => e.VehicleId == entity.VehicleId);
            if (vehicle == null)
            {
                response = ServerResponse.BadRequest;
                response.RespDesc = "Vehicle is not found";
                return response;
            }
            _repository.Update(model);
            return response;
        }
    }
}
