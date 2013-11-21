using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TaxiOrNot.Data;
using TaxiOrNot.ResponseModels;
using TaxiOrNot.RestApi.Models;

namespace TaxiOrNot.RestApi.Controllers
{
    public class UsersController:BaseApiController
    {
        [HttpGet]
        public UserModel GetByPhoneId()
        {
            return this.ExecuteOperationAndHandleException(() =>
            {

                var phoneId = this.GetPhoneIdHeaderValue();
                var context = new TaxiOrNotDbContext();

                var user = this.GetUserByPhoneId(phoneId, context);
                return new UserModel()
                {
                    Username = user.Username
                };
            });
        }

        [HttpPut]
        public UserModel ChangeUsername(UserModel model)
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                this.ValidateUsername(model.Username);
                var phoneId = this.GetPhoneIdHeaderValue();
                var context = new TaxiOrNotDbContext();
                var user = this.GetUserByPhoneId(phoneId, context);
                user.Username = model.Username;
                context.SaveChanges();
                return new UserModel()
                {
                    Username = user.Username
                };
            });
        }

        private void ValidateUsername(string username)
        {
        }
    }
}