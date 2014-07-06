using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ASP.NET_MVC_App.Models;
using PreEmptive.Analytics.Common;

namespace ASP.NET_MVC_App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            var client = PAClientFactory.GetPAClient();

            // Optional step: Configure the behavior of the message queue.
            var flowController = new FlowController();
            flowController.QueueSize = 100;
            flowController.HighWater = 50;
            //flowController.SendDisabled = true;


            // The use of the binary info and all of its fields are optional.
            var binaryInfo = new BinaryInfo();
            binaryInfo.MethodName = "AppStart";
            binaryInfo.ClassName = "MainPage";

            client.ApplicationStart(null, binaryInfo, flowController);

            // If you want to use the default values this is the call you can use. Default values are documented in the user guide.
            // client.ApplicationStart();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var client = PAClientFactory.GetPAClient();
            client.ReportException(ExceptionInfo.Uncaught(exception));
        }

        protected void Application_End()
        {
            PAClientFactory.GetPAClient().ApplicationStop();
        }
    }
}