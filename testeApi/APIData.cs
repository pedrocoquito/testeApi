using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeApi
{
    public class APIData
    {
        [JsonProperty("items")]
        public List<Person> Items { get; set; }
        public APIData(List<Person> items)
        {
            Items = items;
        }
    }
}
