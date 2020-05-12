using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleTrackingSystem.DataAccess.Repository;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Entities;

namespace VehicleTrackingSystem.Domain.Services
{
    public class DeviceManagement : IDeviceManagement
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Device> _repository;
        private readonly IMapper _mapper;
        public DeviceManagement(IUnitOfWork unitOfWork, IRepository<Device> repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }
        public bool DeviceExists(Guid number)
        {
            return _repository.Get().Any(e => e.ImeiNumber == number); 
        }
        public List<DeviceViewModel> GetAllDevices()
        {
            var result = _repository.Get();
            var list = _mapper.Map<List<DeviceViewModel>>(result);
            return list;
        }
        public void AddDevice(DeviceViewModel entity)
        {
            var model = _mapper.Map<Device>(entity);
            _repository.Add(model);
            _unitOfWork.Commit();
        }
    }
}
