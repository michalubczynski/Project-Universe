namespace Universe.Models
{
    public class StarSystemDiscoverers
    {
        public int StarSystemId { get; set; }
        public Planet StarSystem { get; set; }
        public int DiscovererId { get; set; }
        public Discoverer Discoverer { get; set; }
    }
}
