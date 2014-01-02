using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxiOrNot.ResponseModels
{
    public class TaxiModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }

    public class TaxiDetailsModel : TaxiModel
    {
        public string Description { get; set; }

        public decimal DailyKmFare { get; set; }

        public decimal DailyBookingFare { get; set; }

        public decimal DailyInitialFare { get; set; }

        public decimal DailyMinFare { get; set; }

        public decimal NightlyMinFare { get; set; }

        public decimal NightlyKmFare { get; set; }

        public decimal NightlyBookingFare { get; set; }

        public decimal NightlyInitialFare { get; set; }

        public IQueryable<CommentModel> Comments { get; set; }

        public string Telephone { get; set; }

        public string WebSite { get; set; }

        public bool Liked { get; set; }

        public bool Disliked { get; set; }
    }

    public class NewTaxiModel
    {

        public string Name { get; set; }

        public string Telephone { get; set; }
        public string WebSite { get; set; }

        public decimal DailyKmFare { get; set; }

        public decimal DailyBookingFare { get; set; }

        public decimal DailyInitialFare { get; set; }

        public decimal DailyMinFare { get; set; }

        public decimal NightlyMinFare { get; set; }

        public decimal NightlyKmFare { get; set; }

        public decimal NightlyBookingFare { get; set; }

        public decimal NightlyInitialFare { get; set; }

        public CityDetailsModel City { get; set; }
    }
}