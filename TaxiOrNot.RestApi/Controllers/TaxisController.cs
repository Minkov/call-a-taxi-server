using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaxiOrNot.Data;
using TaxiOrNot.EntityModels;
using TaxiOrNot.ResponseModels;
using TaxiOrNot.RestApi.Models;

namespace TaxiOrNot.RestApi.Controllers
{
    public class TaxisController : BaseApiController
    {
        [HttpGet]
        public TaxiDetailsModel GetById(int taxiId)
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                var context = new TaxiOrNotDbContext();


                var taxiEntity = this.GetTaxiEntityById(taxiId, context);


                var taxiModel = Parser.ToTaxiDetailsModel(taxiEntity);

                var phoneId = this.GetPhoneIdHeaderValue();
                var theUser = context.Users.FirstOrDefault(u => u.PhoneId == phoneId);


                var taxiVoteByUser = taxiEntity.Votes.FirstOrDefault(v => v.UserId == theUser.Id);
                if (taxiVoteByUser!=null)
                {
                    taxiModel.Liked = taxiVoteByUser.VoteType.Type == "liked";
                    taxiModel.Disliked = taxiVoteByUser.VoteType.Type == "disliked";
                }

                return taxiModel;
            });
        }

        [HttpPost]
        public HttpResponseMessage PostTaxi(NewTaxiModel model)
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                this.ValidateTaxi(model);

                var context = new TaxiOrNotDbContext();

                if (context.Taxis.Any(t => t.Name.ToLower() == model.Name.ToLower() &&
                                           t.City.Name.ToLower() == model.City.Name.ToLower() &&
                                           t.City.Country.ToLower() == model.City.Country.ToLower()))
                {
                    throw new InvalidOperationException("The taxi is already in the database");
                }

                var taxiEntity = Parser.ToEntity(model);

                var city = context.Cities.FirstOrDefault(c => c.Name.ToLower() == model.City.Name.ToLower() &&
                                                              c.Country.ToLower() == model.City.Country.ToLower());
                if (city == null)
                {
                    city = new City()
                    {
                        Name = model.City.Name,
                        Country = model.City.Country,
                        Latitude = model.City.Latitude,
                        Longitude = model.City.Longitude
                    };
                }

                taxiEntity.City = city;

                context.Taxis.Add(taxiEntity);
                context.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("TaxisApi", new { taxiId = taxiEntity.Id }));
                return response;
            });
        }

        [HttpPut]
        [ActionName("comment")]
        public HttpResponseMessage PutCommentTaxi(int taxiId, NewCommentModel model)
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                var context = new TaxiOrNotDbContext();
                var taxiEntity = this.GetTaxiEntityById(taxiId, context);

                var userPhoneId = this.GetPhoneIdHeaderValue();
                var user = this.GetUserByPhoneId(userPhoneId, context);

                this.ValidateComment(model);

                var comment = new Comment()
                {
                    Text = model.Text,
                    User = user,
                    Taxi = taxiEntity
                };
                context.Comments.Add(comment);

                context.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("TaxisApi", new { taxiId = taxiEntity.Id }));

                return response;
            });
        }

        [HttpPut]
        [ActionName("like")]
        public HttpResponseMessage PutLikeTaxi(int taxiId)
        {
            return this.PlaceVote(taxiId, "liked", this.GetPhoneIdHeaderValue());
            //return this.ExecuteOperationAndHandleException(() =>
            //{
            //    var context = new TaxiOrNotDbContext();

            //    Taxi taxiEntity = GetTaxiEntityById(taxiId, context);

            //    var userPhoneId = this.GetPhoneIdHeaderValue();
            //    var user = GetUserByPhoneId(userPhoneId, context);

            //    if (taxiEntity.Votes.Any(v => v.UserId == user.Id))
            //    {
            //        throw new InvalidOperationException("A user can vote only once for a taxi");
            //    }

            //    var likedVoteType = context.VoteTypes.FirstOrDefault(vt => vt.Type == "liked");
            //    var vote = new Vote()
            //    {
            //        Taxi = taxiEntity,
            //        User = user,
            //        VoteType = likedVoteType
            //    };

            //    context.Votes.Add(vote);
            //    context.SaveChanges();

            //    var response = this.Request.CreateResponse(HttpStatusCode.Created);
            //    response.Headers.Location = new Uri(Url.Link("TaxisApi", new { taxiId = taxiEntity.Id }));

            //    return response;
            //});
        }

        [HttpPut]
        [ActionName("dislike")]
        public HttpResponseMessage PutDislikeTaxi(int taxiId)
        {
            return this.PlaceVote(taxiId, "disliked", this.GetPhoneIdHeaderValue());
        }

        private HttpResponseMessage PlaceVote(int taxiId, string type, string userPhoneId)
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                var context = new TaxiOrNotDbContext();

                Taxi taxiEntity = GetTaxiEntityById(taxiId, context);

                var user = this.GetUserByPhoneId(userPhoneId, context);

                if (taxiEntity.Votes.Any(v => v.UserId == user.Id))
                {
                    throw new InvalidOperationException("A user can vote only once for a taxi");
                }

                var likedVoteType = context.VoteTypes.FirstOrDefault(vt => vt.Type == type);
                var vote = new Vote()
                {
                    Taxi = taxiEntity,
                    User = user,
                    VoteType = likedVoteType
                };

                context.Votes.Add(vote);
                context.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("TaxisApi", new { taxiId = taxiEntity.Id }));

                return response;
            });
        }

        private Taxi GetTaxiEntityById(int taxiId, TaxiOrNotDbContext context)
        {
            if (taxiId <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid Taxi ID");
            }
            var taxiEntity = context.Taxis.FirstOrDefault(t => t.Id == taxiId);
            if (taxiEntity == null)
            {
                throw new ArgumentOutOfRangeException("Invalid Taxi ID");
            }
            return taxiEntity;
        }

        //TODO
        private void ValidateTaxi(NewTaxiModel taxi)
        {
        }

        //TODO
        private void ValidateComment(NewCommentModel comment)
        {
        }
    }
}