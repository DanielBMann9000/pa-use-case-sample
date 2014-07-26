using PreEmptive.Analytics.Common;
#if Android
using PreEmptive.Analytics.XamarinAndroid;
#elif iOS
using PreEmptive.Analytics.XamariniOS;
#endif

namespace Xamarin.Forms
{
    public static class PAClientFactory
    {
        private static PAClient Client;

		public static void StopApplication(bool immediate=false)
		{
			GetPAClient ().ApplicationStop (immediate:immediate);
		}
		public static void StartApplication(string instance,string username)
		{
			GetPAClient (instance,username).ApplicationStart ();
		}
		public static void FeatureTick(string name)
		{
			GetPAClient ().FeatureTick (name);
		}
        public static void FeatureTick(string name, ExtendedKeys keys)
        {
            GetPAClient().FeatureTick(name, keys);
        }
		public static void StartFeature(string name)
		{
			GetPAClient ().FeatureStart (name);
		}
		public static void StopFeature(string name, ExtendedKeys keys=null)
		{
			GetPAClient ().FeatureStop (name,keys);
		}
		public static void Exception(System.Exception ex,ExceptionType exType)
		{
			GetPAClient ().ReportException (new ExceptionInfo {
				Comment="user comment",
				Contact="TODO:Get UserId",
				Exception=ex,
				ExceptionType=exType,
				Message=ex.Message
			});
		}
		public static void Exception(System.Exception ex)
		{
			Exception (ex, ExceptionType.Uncaught);
		}

		public static void OnEventFeatureTick(Button element,string name=null)
		{
			element.Clicked += (s, e) => GetPAClient ().FeatureTick ((name ?? element.Text));
		}


		private static PAClient GetPAClient()
		{
			if (Client == null) {
				Client = GetPAClient (null);
					}
			return Client;
		}
		public static PAClient GetPAClient(string instance=null,string username=null)
        {
            if (Client == null)
            {
                // Required step: create the configuration structure and populate it
                // This tells the server who is sending the messages via the company and application IDs.
                // There are lots of other configuration settings that control the content and sending of messages.
                // The first GUID is the company ID provided by PreEmptive Solutions.
                // The second Guid is provided by you to identify the application.
                // This is the endpoint described here http://www.preemptive.com/support/resources/ris-ce
                var configuration = new Configuration("1d2b02e0-064d-49a0-bc1b-4be4381c62d3", "42AC2020-ABA1-9069-A2BD-98072B33309A");

                // Optional configuration
                configuration.CompanyName = "PreEmptive Solutions";
#if Android
				configuration.ApplicationName = "PreEmptive Analytics Sample";
                configuration.ApplicationType = "Android Sample";
#elif iOS
                configuration.ApplicationName = "iOS Xamarin Sample App";
                configuration.ApplicationType = "iOS Sample";
#endif
				configuration.InstanceID = instance;
                configuration.GeneratedUserName = username;
                configuration.ApplicationVersion = "1.0";
				configuration.Endpoint = "josh-2012r2-2.preemptive.internal/endpoint";
                configuration.UseSSL = false;
                configuration.FullData = true;
				configuration.StopBehavior.SessionExtensionWindow = 5000;
				configuration.SupportOfflineStorage = true;
                //configuration.SupportOfflineStorage = true;
                //configuration.OmitPersonalInfo = true;

                Client = new PAClient(configuration);
                


            }

            return Client;
        }

    }
}
