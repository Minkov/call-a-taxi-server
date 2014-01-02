using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TaxiOrNot.Data;

namespace TaxiOrNot.RestApi.Controllers
{
    public class InitController : BaseApiController
    {
        [HttpGet]
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
                catch
                {
                }
                return "Server is up and running";
            });
        }
    }
}