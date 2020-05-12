using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleTrackingSystem.DataObjects.Domain;

namespace VehicleTrackingSystem.Simulator.Handlers
{
    public interface IRequestHandler<T> where T : class
    {
        Task<HttpResponseMessage> ProcessingRequest(T request,string method);
        Task<string> ProcessingRequest(string method);
    }
}
