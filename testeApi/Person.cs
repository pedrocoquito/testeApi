using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeApi
{
    public class Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nome")]
        public string Name { get; set; }
        [JsonProperty("tem_curriculo")]
        public bool HasResume { get; set; }
        [JsonProperty("link_api")]
        public string ResumeLink { get; set; }
        public string Resume { get; set; }
        public Person()
        {

        }
        public Person(int id, string name, bool hasResume, string resumeLink)
        {
            Id = id;
            Name = name;
            HasResume = hasResume;
            ResumeLink = resumeLink;
        }
    }
}
