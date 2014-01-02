using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiOrNot.Data;
using TaxiOrNot.ResponseModels;
using TaxiOrNot.RestApi.Models;

namespace TaxiOrNot.RestApi.Controllers
{
    public class CitiesController : BaseApiController
    {
        [HttpGet]
        public IQueryable<CityModel> GetAll()
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                var context = new TaxiOrNotDbContext();
                return context.Cities
                              .OrderBy(c => c.Name.ToLower())
                              .ThenBy(c => c.Country.ToLower())
                              .Select(Parser.ToCityModel);
            });
        }

        [HttpGet]
        public IQueryable<CityModel> GetAll(string location)
        {
            var locationModel = this.ParseLocation(location);

            var context = new TaxiOrNotDbContext();
            return context.Cities
                    .OrderBy(c => (c.Latitude - locationModel.Latitude) * (c.Latitude - locationModel.Latitude) +
                        (c.Longitude - locationModel.Longitude) * (c.Longitude - locationModel.Longitude))
                        .Select(Parser.ToCityModel);
        }

        [HttpGet]
        public CityDetailsModel GetById(int cityId)
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                if (cityId <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid City ID");
                }
                var context = new TaxiOrNotDbContext();
                var cityEntity = context.Cities.FirstOrDefault(c => c.Id == cityId);
                if (cityEntity == null)
                {
                    throw new ArgumentOutOfRangeException("Invalid City ID");
                }
                return Parser.ToCityDetailsModel(cityEntity);
            });
        }


        private LocationModel ParseLocation(string location)
        {
            var locationArgs = location.Split(';');
            if (locationArgs.Length != 2)
            {
                throw new ArgumentOutOfRangeException("Invalid location");
            }
            return new LocationModel()
            {
                Latitude = decimal.Parse(locationArgs[0]),
                Longitude = decimal.Parse(locationArgs[1]),
            };
        }
    }
}