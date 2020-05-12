using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTrackingSystem.DataObjects.Abstraction
{
    public class ServerResponse : BaseRequest
    {
        public static ServerResponse OK = new ServerResponse { RespCode = 200 };
        public static ServerResponse ERROR = new ServerResponse { RespCode = 500 };
        public static ServerResponse BadRequest = new ServerResponse { RespCode = 400 };
        public static ServerResponse Unauthorized = new ServerResponse { RespCode = 401 };
        public static ServerResponse Forbidden = new ServerResponse { RespCode = 403 };
        public static ServerResponse NotFound = new ServerResponse { RespCode = 404 };

        public int RespCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RespDesc { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; set; }
    }
}
