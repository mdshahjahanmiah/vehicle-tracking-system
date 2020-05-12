using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Simulator.Handlers
{
    public class HttpHandler : IHttpHandler
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _settings;
        public HttpHandler(IHttpClientFactory clientFactory, AppSettings settings)
        {
            _clientFactory = clientFactory;
            _settings = settings;
        }
        public async Task<HttpResponseMessage> GetRequestResolver(string method)
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(_settings.ServerUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(method);
            return response;
        }

        public async Task<HttpResponseMessage> PostRequestResolver(string request, string method)
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(_settings.ServerUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string mediaType = "application/json";
            var content = new StringContent(request, Encoding.UTF8, mediaType);
            var response = await client.PostAsync(method, content);
            return response;
        }
    }
}
