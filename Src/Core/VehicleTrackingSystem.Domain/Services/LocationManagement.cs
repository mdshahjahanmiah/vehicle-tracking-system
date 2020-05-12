using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleTrackingSystem.DataAccess.Repository;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Entities;
using VehicleTrackingSystem.Utilities.ExternalServices;

namespace VehicleTrackingSystem.Domain.Services
{
    public class LocationManagement : ILocationManagement
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Location> _repository;
        private readonly IMapper _mapper;
        private readonly IGeocoding _geocodingHandler;
        public LocationManagement(IUnitOfWork unitOfWork, IRepository<Location> repository, IMapper mapper, IGeocoding geocodingHandler)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
            _geocodingHandler = geocodingHandler;
        }

        public List<LocationViewModel> GetAllLocations()
        {
            var result = _repository.Get();
            var list = _mapper.Map<List<LocationViewModel>>(result);
            return list;
        }

        public ServerResponse RecordLocation(LocationViewModel entity)
        {
            entity.LocationId = 0;
            var response = ServerResponse.OK;
            var model = _mapper.Map<Location>(entity);
            _repository.Add(model);
            _unitOfWork.Commit();
            return response;
        }
        public List<LocationViewModel> RetriveLocation(LocationViewModel entity)
        {
            var result = (IEnumerable<Location>)null;
            if (entity.VehicleId == 0) result = _repository.Get(); 
            else if ( entity.DateFrom != DateTime.MinValue && entity.DateTo != DateTime.MinValue) result = LocationBetweenDates(entity);
            else result = CurrentLocation(entity);
            var response = _mapper.Map<List<LocationViewModel>>(result);
            var responseWithLocality = SetLocality(response);
            return responseWithLocality;
        }
        private IEnumerable<Location> CurrentLocation(LocationViewModel entity)
        {
            return _repository.Get().Where(t => t.VehicleId == entity.VehicleId).OrderByDescending(x => x.CreatedTime).Take(1);
        }
        private IEnumerable<Location> LocationBetweenDates(LocationViewModel entity)
        {
            return (from a in _repository.Get()
                    where a.VehicleId == entity.VehicleId &&
                    (a.CreatedTime >= entity.DateFrom && a.CreatedTime <= entity.DateTo)
                    select a).ToList();
        }
        private List<LocationViewModel> SetLocality(List<LocationViewModel> list)
        {
            foreach (var locality in list)
            {
                locality.Locality = _geocodingHandler.GetLocality(locality.Latitude,locality.Longitude);
            }
            return list;
        }
    }
}
