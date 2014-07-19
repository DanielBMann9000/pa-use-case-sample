using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;

namespace FormsGallery.Droid
{
	[Activity(Label = "PreEmptive Analytics Use Case Gallery ", MainLauncher = true)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            

            SetPage(App.GetMainPage());
          
			AndroidEnvironment.UnhandledExceptionRaiser += (s, e) => {
				Xamarin.Forms.PAClientFactory.Exception(e.Exception);
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

