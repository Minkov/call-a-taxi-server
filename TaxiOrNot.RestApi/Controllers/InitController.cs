using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiOrNot.Data;

namespace TaxiOrNot.RestApi.Controllers
{
    public class InitController : BaseApiController
    {
        public string WakeUpServer()
        {
            return this.ExecuteOperationAndHandleException(() =>
            {
                try
                {
                    var phoneId = this.GetPhoneIdHeaderValue();
                    var context = new TaxiOrNotDbContext();
                    this.GetUserByPhoneId(phoneId, context);
                }
                return "Server is up and running";
            });
        }
    }
}