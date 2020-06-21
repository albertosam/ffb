using System;
using System.Collections.Generic;
using System.Text;

namespace MultiAgentes.Lib.Requests
{
    public class JsonContent : System.Net.Http.StringContent
    {
        public JsonContent(object value)
            : base(Newtonsoft.Json.JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(object value, string mediaType)
            : base(Newtonsoft.Json.JsonConvert.SerializeObject(value), Encoding.UTF8, mediaType)
        {
        }
    }
}
