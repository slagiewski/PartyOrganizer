using Newtonsoft.Json;

namespace PartyOrganizer.Core.Model
{

    public class LocationData
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Lat: {Lat}, Lng: {Lng}, Name: {Name}";
        }

    }
}