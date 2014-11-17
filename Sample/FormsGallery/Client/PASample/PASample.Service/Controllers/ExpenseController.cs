using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PreEmptive.Analytics.Common;

namespace PASample.Service.Controllers
{
    public class ExpenseController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpPost]
        public Models.ExpenseApprovalResponse Approve(Models.ExpenseApprovalRequest request)
        {
            var keys = new ExtendedKeys();
            keys.Add("TransactinoId", request.Id.ToString());
            keys.Add("Requestor", request.InstanceId);
            keys.Add("Amount", request.Ammount);
            keys.Add("Reason", request.Reason);
            PAClientFactory.GetPAClient().FeatureStart("Expense Approval - Service",keys);
            var resp = new Models.ExpenseApprovalResponse();
            var stopKeys = new ExtendedKeys();
            if (request.Ammount>=5000)
            {
                resp.Exception = new Models.ExceptionModel
                {
                    Message = "Amount exceeds allowable request"
                };
                stopKeys.Add("Approved", 0);

            }else
            {
                stopKeys.Add("Approved", 1);

            }
            PAClientFactory.GetPAClient().FeatureStop("Expense Approval - Service",stopKeys);
            return resp;
        }

       
        // DELETE api/values/5
        public void Delete(Guid id)
        {
        }
    }
}
