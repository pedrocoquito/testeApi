using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeApi
{
    public class AcessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        public AcessToken(string token)
        {
            Token = token;
        }
    }
}
