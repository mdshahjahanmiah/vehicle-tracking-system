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

namespace VehicleTrackingSystem.Simulator.Controllers
{
    public class VehicleController : Controller
    {
        private readonly RequestControllerHelper _controllerHelper;
        private readonly IRequestHandler<VehicleViewModel>  _requestHandler;
        private readonly ICacheController _cache;
        private readonly IMemoryCache _memoryCache;
        public VehicleController(IRequestHandler<VehicleViewModel> requestHandler,ICacheController cache, IMemoryCache memoryCache)
        {
            _controllerHelper = new RequestControllerHelper();
            _requestHandler = requestHandler;
            _cache = cache;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult AddVehicle(int vehicleId, string name, string maker, string model, string year, string bodyType, int userId, int deviceId, Guid imeiNumber, string deviceName, bool status)
        {
            var request = new VehicleViewModel();
            request.Device = new DeviceViewModel();
            if (vehicleId == 0){
                request = _controllerHelper.VehicleRequest();
            }
            else {
                request.VehicleId = vehicleId;
                request.Name = name;
                request.Maker = maker;
                request.Model = model;
                request.Year = year;
                request.BodyType = bodyType;
                request.DeviceId = deviceId;
                request.Device.DeviceId = deviceId;
                request.Device.ImeiNumber = imeiNumber;
                request.Device.Name = deviceName;
                request.Device.Status = status;
            }
            var result = _requestHandler.ProcessingRequest(ActionMethodType.GetAllVehiclesWithDevices);
            var listOfVehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(result.Result);
            ViewBag.ListOfVehicles = listOfVehicles;
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> AddVehicle(VehicleViewModel request, string answer)
        {
            if (!string.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Submit":
                        try
                        {
                            request.Token = _cache.Get("Token-" + _memoryCache.Get<string>("UserId"));
                            request.DeviceId = request.Device.DeviceId;
                            var result = await _requestHandler.ProcessingRequest(request, ActionMethodType.RegisterVehicle);
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
                        return RedirectToAction("AddVehicle");
                }
            }
            var getAllVehiclesWithDevices = _requestHandler.ProcessingRequest(ActionMethodType.GetAllVehiclesWithDevices);
            var listOfVehicles = JsonConvert.DeserializeObject<List<VehicleViewModel>>(getAllVehiclesWithDevices.Result);
            ViewBag.ListOfVehicles = listOfVehicles;
            return View(request);
        }
    }
}