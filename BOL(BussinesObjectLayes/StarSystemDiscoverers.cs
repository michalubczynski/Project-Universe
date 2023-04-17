using Universe.Models.starsystem;

namespace Universe.Models.discoverer
{
    public class StarSystemDiscoverers
    {
        public int StarSystemId { get; set; }
        public StarSystem StarSystem { get; set; }
        public int DiscovererId { get; set; }
        public Discoverer Discoverer { get; set; }
    }
}
