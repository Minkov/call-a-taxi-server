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

namespace TaxiOrNot.RestApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected T ExecuteOperationAndHandleException<T>(Func<T> operation)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(errResponse);
            }
        }

        //TODO create user
        protected User CreateUser(string userPhoneId, string username, TaxiOrNotDbContext context)
        {
            var user = new User()
            {
                Username = username,
                PhoneId = userPhoneId
            };

            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        protected string GetPhoneIdHeaderValue()
        {
            var userPhoneId = this.Request.Headers.GetValues("x-phoneId").First();
            return userPhoneId;
        }

        protected User GetUserByPhoneId(string userPhoneId, TaxiOrNotDbContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.PhoneId == userPhoneId);
            if (user == null)
            {
                user = this.CreateUser(userPhoneId, "anonymous", context);
            }
            return user;
        }
    }
}