namespace PartyOrganizer.Core.Model
{

    public class LocationData
    {
        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Lat: {Lat}, Lng: {Lng}, Name: {Name}";
        }

    }
}