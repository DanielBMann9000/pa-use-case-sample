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

namespace FormsGallery.Droid
{
	[Activity(Label = "PreEmptive Analytics Use Case Gallery ", MainLauncher = true)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            App.RegisterGeocoder(new Geocoder(this));
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

    
    class Geocoder : IGeocoder
    {
        private Android.Content.Context _ctx;
        public Geocoder(Android.Content.Context ctx)
        {
            this._ctx = ctx;
        }

        public  void GetCurrentLocation(Action<Cordinate> action)
        {
            var locator = new Geolocator(_ctx) { DesiredAccuracy = 50 };
            
            
            locator.GetPositionAsync(timeout: 10000).ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    action(new Cordinate
                    {
                        latitude = t.Result.Latitude,
                        longitude = t.Result.Longitude

                    });
                }

                

            }
            );
        }
    }
}

