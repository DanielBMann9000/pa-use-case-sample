using System;
using System.Threading.Tasks;
using PreEmptive.Analytics.Common;
using Xamarin.Forms;

namespace FormsGallery
{
    class PerformancePage : BasePage
    {
		/*I don't like having the button state stored in a static but how do I share the state as the user goes from page to page*/
		private static bool _isFeaturePressed=false;

        public PerformancePage()
        {
            Label header = new Label
            {
				Text = "Timing inside and out",
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
            };

			var startButton = new Button {
				Text = "Start",
				HorizontalOptions = LayoutOptions.Start,
				IsEnabled=!_isFeaturePressed


			};
			var stopButton = new Button {
				Text = "Stop",
				HorizontalOptions = LayoutOptions.Start,
				IsEnabled=_isFeaturePressed
			};
			startButton.Clicked += (s, e) => {
				stopButton.IsEnabled=true;
				startButton.IsEnabled=false;
				PAClientFactory.StartFeature("Performance.Button");
				_isFeaturePressed=true;
			};
			stopButton.Clicked += (s, e) => {
				stopButton.IsEnabled=false;
				startButton.IsEnabled=true;
				PAClientFactory.StopFeature("Performance.Button");
				_isFeaturePressed=false;
			};

			var serviceButton =new  Button {
					Text = "Go"
			
				};

            var svcLabel = new Label();
            serviceButton.Clicked +=  (s, e) =>
            {
                PAClientFactory.StartFeature("Performance.Geolocate");
                App.Geocoder.GetCurrentLocation((c) =>
                {
                    svcLabel.Text = "Geocoding completed";

                    var keys = new ExtendedKeys();
                    keys.Add("Latitude", c.latitude);
                    keys.Add("longitude", c.longitude);
                    PAClientFactory.StopFeature("Performance.Geolocate",keys);

                });
               
                


			};

            StackLayout stackLayout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = 
                {
                 
                    new StackLayout
                    {
                        Spacing = 0,

                        Orientation = StackOrientation.Horizontal,
                        Children = 
                        {
                            new Label
                            {
                                Text = "User Behavior",
								HorizontalOptions=LayoutOptions.CenterAndExpand
                            },
							startButton,
							stopButton
                        }
                    }
                    
                    //,new StackLayout
                    //{
                    //    Orientation=StackOrientation.Horizontal,
                    //    Children=
                    //    {
                    //        new Label{
                    //            Text="Service Level",
                    //            HorizontalOptions=LayoutOptions.CenterAndExpand
                    //        },
                    //        serviceButton,
                    //        svcLabel

                    //    }

                    //}
                }
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    stackLayout
                }
            };
        }
    }
}
