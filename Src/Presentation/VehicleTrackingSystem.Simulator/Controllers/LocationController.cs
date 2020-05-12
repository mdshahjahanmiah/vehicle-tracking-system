using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using VehicleTrackingSystem.CacheManagement.Caching;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Simulator.Enums;
using VehicleTrackingSystem.Simulator.Handlers;
using VehicleTrackingSystem.Simulator.Helpers;
using VehicleTrackingSystem.Utilities.ExternalServices;

namespace VehicleTrackingSystem.Simulator.Controllers
{
    public class LocationController : Controller
    {
        private readonly RequestControllerHelper _controllerHelper;
        private readonly IRequestHandler<LocationViewModel> _requestHandler;
        private readonly ICacheController _cache;
        private readonly IMemoryCache _memoryCache;
        
        public LocationController(IRequestHandler<LocationViewModel> requestHandler, ICacheController cache, IMemoryCache memoryCache)
        {
            _controllerHelper = new RequestControllerHelper();
            _requestHandler = requestHandler;
            _cache = cache;
            _memoryCache = memoryCache;
           
        }

        [HttpGet]
        public IActionResult AddLocation(int locationId,double latitude, double longitude, double altitude, double horizontalAccuracy, double verticalAccuracy, double speed, DateTime createdTime, int VehicleId)
        {
            var model = new LocationViewModel();
            if (locationId == 0)
            {
                model = _controllerHelper.LocationRequest();
            }
            else
            {
                model.LocationId = locationId;
                model.Latitude = latitude;
                model.Longitude = longitude;
                model.Altitude = altitude;
                model.HorizontalAccuracy = horizontalAccuracy;
                model.VerticalAccuracy = verticalAccuracy;
                model.Speed = speed;
                model.CreatedTime = createdTime;
                model.VehicleId = VehicleId;
            }            
            var result = _requestHandler.ProcessingRequest(ActionMethodType.GetAllVehicles);
            var listOfVehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(result.Result);
            ViewBag.ListOfVehicles = listOfVehicles;

            var locationResult = _requestHandler.ProcessingRequest(ActionMethodType.GetAllLocations);
            var listOfLocations = JsonConvert.DeserializeObject<List<LocationViewModel>>(locationResult.Result);
            ViewBag.listOfLocations = listOfLocations;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationViewModel request, string answer)
        {
            if (!string.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Submit":
                        try
                        {
                            request.Token = _cache.Get("Token-" + _memoryCache.Get<string>("UserId"));
                            var result = await _requestHandler.ProcessingRequest(request, ActionMethodType.RecordLocation);
                            var response = result.Content.ReadAsStringAsync();
                            var serverResponse = JsonConvert.DeserializeObject<ServerResponse>(response.Result);
                            if (serverResponse.RespCode == 200) ViewBag.Status = "is successful.";
                            else ViewBag.Status = serverResponse.RespDesc;
                        }
                        catch (RedisConnectionException redisConnectionException)
                        {
                            ViewBag.Status = "Oops! Please contact with administrator for connecting Redis Server.";
                        }
                        break;
                    case "Reset":
                        return RedirectToAction("AddLocation");
                }
            }
            var vehicles = _requestHandler.ProcessingRequest(ActionMethodType.GetAllVehicles);
            var listOfVehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(vehicles.Result);
            ViewBag.ListOfVehicles = listOfVehicles;

            var locationResult = _requestHandler.ProcessingRequest(ActionMethodType.GetAllLocations);
            var listOfLocations = JsonConvert.DeserializeObject<List<LocationViewModel>>(locationResult.Result);
            ViewBag.listOfLocations = listOfLocations;

            return View(request);
        }

        [HttpGet]
        public IActionResult CurrentLocation()
        {
            var result = _requestHandler.ProcessingRequest(ActionMethodType.GetAllVehicles);
            var listOfVehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(result.Result);
            ViewBag.ListOfVehicles = listOfVehicles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CurrentLocation(LocationViewModel request, string answer)
        {
            if (!string.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Submit":
                        try
                        {
                            request.Token = _cache.Get("Token-" + _memoryCache.Get<string>("UserId"));
                            var result = await _requestHandler.ProcessingRequest(request, ActionMethodType.GetCurrentLocation);
                            var response = result.Content.ReadAsStringAsync();
                            var serverResponse = JsonConvert.DeserializeObject<ServerResponse>(response.Result);
                            if (serverResponse.RespCode == 200)
                            {
                                var list = JsonConvert.DeserializeObject<List<LocationViewModel>>(serverResponse.Result.ToString());
                                ViewBag.ListOfLocations = list;
                            }
                            else ViewBag.Status = serverResponse.RespDesc;
                        }
                        catch (RedisConnectionException redisConnectionException)
                        {
                            ViewBag.Status = "Oops! Please contact with administrator for connecting Redis Server.";
                        }
                        break;
                    case "Reset":
                        return RedirectToAction("AddLocation");
                }
            }
            var vehicles = _requestHandler.ProcessingRequest(ActionMethodType.GetAllVehicles);
            var listOfVehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(vehicles.Result);
            ViewBag.ListOfVehicles = listOfVehicles;
            return View(request);
        }
    }
}