using Newtonsoft.Json;
using System;

namespace MsaJoshuaKim.Models
{
    public class Analyze_faces
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        public DateTime storeDate { get; set; }
        public string analyzeFace { get; set; }
    }
}

