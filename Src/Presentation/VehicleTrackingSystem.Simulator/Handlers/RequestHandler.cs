using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Simulator.Enums;

namespace VehicleTrackingSystem.Simulator.Handlers
{
    public class RequestHandler<T> : IRequestHandler<T> where T : class
    {
        private readonly IHttpHandler _httpClientHandler;
        public RequestHandler(IHttpHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> ProcessingRequest(T request,string method)
        {
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            var result = await _httpClientHandler.PostRequestResolver(json, method);
            return result;
        }

        public async Task<string> ProcessingRequest(string method)
        {
            var result = await _httpClientHandler.GetRequestResolver(method);
            var response = result.Content.ReadAsStringAsync();
            return response.Result;
        }
    }
}
