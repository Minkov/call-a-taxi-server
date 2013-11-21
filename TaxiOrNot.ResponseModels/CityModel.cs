using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiOrNot.ResponseModels
{
    public class CityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CityDetailsModel : CityModel
    {

        public string Country { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public IQueryable<TaxiModel> Taxis { get; set; }

    }
}
