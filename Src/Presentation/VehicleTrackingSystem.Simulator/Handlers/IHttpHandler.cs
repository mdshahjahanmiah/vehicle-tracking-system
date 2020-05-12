using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Simulator.Handlers
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> PostRequestResolver(string request, string method);
        Task<HttpResponseMessage> GetRequestResolver(string method);
    }
}
