using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiOrNot.EntityModels
{
    public class Taxi
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Telephone { get; set; }

        public string WebSite { get; set; }

        public decimal DailyKmFare { get; set; }

        public decimal DailyBookingFare { get; set; }

        public decimal DailyInitialFare { get; set; }

        public decimal DailyMinFare { get; set; }

        public decimal NightlyKmFare { get; set; }

        public decimal NightlyBookingFare { get; set; }

        public decimal NightlyInitialFare { get; set; }

        public decimal NightlyMinFare { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<TaxiImage> Images { get; set; }

        public Taxi()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
            this.Images = new HashSet<TaxiImage>();
        }
    }
}