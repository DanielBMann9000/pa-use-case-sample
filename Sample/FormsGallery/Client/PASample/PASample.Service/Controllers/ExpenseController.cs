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
        /// <summary>
        /// A fake endpoint which simulates approval / rejection of expense requests. This demonstrates instrumenting
        /// a service using the Feature messages in PA. Two messages are generated one at the start of the method and one
        /// at the end. This allows the workbench to calculate the elaped time between the two messages.
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Models.ExpenseApprovalResponse Approve(Models.ExpenseApprovalRequest request)
        {
            var keys = new ExtendedKeys();
            
            keys.Add("Requestor", request.LicenseKey);
            keys.Add("Department", request.Department);
            keys.Add("Amount", request.Amount);
            keys.Add("Reason", request.Reason);
            PAClientFactory.GetPAClient().FeatureStart("Expense Approval - Service",keys);
            var resp = new Models.ExpenseApprovalResponse();
            var stopKeys = new ExtendedKeys();
            if (request.Amount>=5000)
            {
                resp.Exception = new Models.ExceptionModel
                {
                    Message = "Amount exceeds allowable request"
                };
                stopKeys.Add("RejectedReason", resp.Exception.Message);
                stopKeys.Add("Approved", 0);

            }else
            {
                stopKeys.Add("Approved", 1);

            }
            PAClientFactory.GetPAClient().FeatureStop("Expense Approval - Service",stopKeys);
            return resp;
        }

       
    }
}
