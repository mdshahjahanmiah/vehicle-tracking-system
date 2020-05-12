using System;
using Newtonsoft.Json;

namespace VehicleTrackingSystem.Utilities.ExternalServices
{
    public class GeocodingHandler : IGeocoding
    {
        public GeocodingHandler() { }
        public string GetLocality(double latitude, double longitude)
        {
            try
            {
                var client = new RestSharp.RestClient("https://us1.locationiq.com/");
                var request = new RestSharp.RestRequest("v1/reverse.php", RestSharp.Method.GET);
                request.AddParameter("key", AppKeyConfiguration.Key);
                request.AddParameter("lat", latitude);
                request.AddParameter("lon", longitude);
                request.AddParameter("format", "json");
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<Locality>(response.Content);
                return result.display_name;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            
        }
    }
}
