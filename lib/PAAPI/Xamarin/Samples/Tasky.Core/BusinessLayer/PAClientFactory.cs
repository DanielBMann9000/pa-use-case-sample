using PreEmptive.Analytics.Common;
#if Android
using PreEmptive.Analytics.XamarinAndroid;
#elif iOS
using PreEmptive.Analytics.XamariniOS;
#endif

namespace Tasky.Core
{
    public static class PAClientFactory
    {
#if Android || iOS
        private static PAClient Client;

        public static PAClient GetPAClient()
        {
            if (Client == null)
            {
                // Required step: create the configuration structure and populate it
                // This tells the server who is sending the messages via the company and application IDs.
                // There are lots of other configuration settings that control the content and sending of messages.
                // The first GUID is the company ID provided by PreEmptive Solutions.
                // The second Guid is provided by you to identify the application.
                // This is the endpoint described here http://www.preemptive.com/support/resources/ris-ce
                var configuration = new Configuration("7d2b02e0-064d-49a0-bc1b-4be4381c62d3", "42AC2020-ABA1-9069-A2BD-98072B33309A");

                // Optional configuration
                configuration.CompanyName = "PreEmptive Solutions";
#if Android
                configuration.ApplicationName = "Android Xamarin Sample App";
                configuration.ApplicationType = "Android Sample";
#elif iOS
                configuration.ApplicationName = "iOS Xamarin Sample App";
                configuration.ApplicationType = "iOS Sample";
#endif
                configuration.ApplicationVersion = "1.0";
                configuration.Endpoint = "message.runtimeintelligence.com/PreEmptive.Web.Services.Messaging/MessagingServiceV2.asmx";
                configuration.UseSSL = false;
                configuration.FullData = false;
                //configuration.SupportOfflineStorage = true;
                //configuration.OmitPersonalInfo = true;

                Client = new PAClient(configuration);
            }

            return Client;
        }
#endif
    }
}
