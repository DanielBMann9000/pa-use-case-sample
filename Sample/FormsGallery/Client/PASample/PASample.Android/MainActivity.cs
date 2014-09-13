using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Xamarin.Geolocation;
using System.Threading.Tasks;

namespace PASample.Droid
{
    [Activity(Label = "PA Sample App", MainLauncher = true)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);


            SetPage(App.GetMainPage());
          
			AndroidEnvironment.UnhandledExceptionRaiser += (s, e) => {
				PAClientFactory.Exception(e.Exception);
			};
        }
      
        protected override void OnStart()
        {
            base.OnStart();
            App.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            App.Shutdown();
        }




    }


}

