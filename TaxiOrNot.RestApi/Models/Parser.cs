using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TaxiOrNot.EntityModels;
using TaxiOrNot.ResponseModels;

namespace TaxiOrNot.RestApi.Models
{
    public class Parser
    {
        public static Expression<Func<City, CityModel>> ToCityModel
        {
            get
            {
                return x => new CityModel()
                {
                    Id = x.Id,
                    Name = x.Name
                };
            }
        }

        public static CityDetailsModel ToCityDetailsModel(City entity)
        {
            return new CityDetailsModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Country = entity.Country,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Taxis = entity.Taxis.AsQueryable().Select(Parser.ToTaxiModel)
            };
        }

        public static Expression<Func<Taxi, TaxiModel>> ToTaxiModel
        {
            get
            {
                return x => 
                new TaxiModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Likes = x.Votes.Where(v => v.VoteType.Type == "liked").Count(),
                    Dislikes = x.Votes.Where(v => v.VoteType.Type == "disliked").Count(),
                };
            }
        }

        internal static TaxiDetailsModel ToTaxiDetailsModel(Taxi taxiEntity)
        {
            return new TaxiDetailsModel()
            {
                Id = taxiEntity.Id,
                Name = taxiEntity.Name,
                Description = taxiEntity.Description,
                Telephone = taxiEntity.Telephone,
                WebSite = taxiEntity.WebSite,
                Likes = taxiEntity.Votes.Count(v => v.VoteType.Type == "liked"),
                Dislikes = taxiEntity.Votes.Count(v => v.VoteType.Type == "disliked"),
                Comments = taxiEntity.Comments.AsQueryable().Select(Parser.ToCommentModel),
                DailyKmFare = taxiEntity.DailyKmFare,
                DailyBookingFare = taxiEntity.DailyBookingFare,
                DailyInitialFare = taxiEntity.DailyInitialFare,
                DailyMinFare = taxiEntity.DailyMinFare,
                NightlyKmFare = taxiEntity.NightlyKmFare,
                NightlyBookingFare = taxiEntity.NightlyBookingFare,
                NightlyInitialFare = taxiEntity.NightlyInitialFare,
                NightlyMinFare = taxiEntity.NightlyMinFare
            };
        }

        public static Expression<Func<Comment, CommentModel>> ToCommentModel
        {
            get
            {
                return x => new CommentModel()
                {
                    Text = x.Text,
                    User = x.User.Username
                };
            }
        }
        
        internal static Taxi ToEntity(NewTaxiModel model)
        {
            throw new NotImplementedException();
        }
    }
}