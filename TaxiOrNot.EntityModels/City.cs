using System.Collections.Generic;

namespace TaxiOrNot.EntityModels
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public virtual ICollection<Taxi> Taxis { get; set; }

        public City()
        {
            this.Taxis = new HashSet<Taxi>();
        }
    }
}