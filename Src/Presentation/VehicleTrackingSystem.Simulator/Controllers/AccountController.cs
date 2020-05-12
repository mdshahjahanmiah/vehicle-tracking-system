using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using VehicleTrackingSystem.CacheManagement.Caching;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Simulator.Enums;
using VehicleTrackingSystem.Simulator.Handlers;
using VehicleTrackingSystem.Simulator.Helpers;

namespace VehicleTrackingSystem.Simulator.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRequestHandler<UserViewModel> _requestHandler;
        private readonly ICacheController _cache;
        private readonly IMemoryCache _memoryCache;
        private readonly RequestControllerHelper _controllerHelper;
        public AccountController(IRequestHandler<UserViewModel> requestHandler, ICacheController cache, IMemoryCache memoryCache)
        {
            _requestHandler = requestHandler;
            _cache = cache;
            _memoryCache = memoryCache;
            _controllerHelper = new RequestControllerHelper();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model, string answer)
        {
            if (!string.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Login":
                        var result = await _requestHandler.ProcessingRequest(model, ActionMethodType.DoLogin);
                        var response = result.Content.ReadAsStringAsync();
                        var serverResponse = JsonConvert.DeserializeObject<UserViewModel>(response.Result);
                        if (serverResponse.Token != null)
                        {
                            _cache.Set("Token-" + serverResponse.UserId, serverResponse.Token);
                            _memoryCache.Set<string>("UserId", serverResponse.UserId.ToString());
                            _memoryCache.Set<string>("UserTypeId", serverResponse.UserTypeId.ToString());
                            return RedirectToAction("AddVehicle", "Vehicle");
                        }
                        ViewBag.Status = serverResponse.RespDesc;
                        break;
                    case "Reset":
                        return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.ListOfUserTypes = _controllerHelper.GetUserTypes();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model, string answer)
        {
            if (!string.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Submit":
                        var result = await _requestHandler.ProcessingRequest(model, ActionMethodType.Register);
                        var response = result.Content.ReadAsStringAsync();
                        var serverResponse = JsonConvert.DeserializeObject<UserViewModel>(response.Result);
                        if (serverResponse.RespCode == 200) ViewBag.Status = "is successful.";
                        else ViewBag.Status = serverResponse.RespDesc;
                        break;
                }
            }
            ViewBag.ListOfUserTypes = _controllerHelper.GetUserTypes();
            return View(model);
        }

        
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}