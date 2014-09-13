using System;
using System.Threading.Tasks;
using PreEmptive.Analytics.Common;
using Xamarin.Forms;

namespace PASample
{
    class PerformancePage : BasePage
    {
		/*I don't like having the button state stored in a static but how do I share the state as the user goes from page to page*/
		private static bool _isFeaturePressed=false;

        public PerformancePage()
        {
            this.Title = "Timing inside and out";
            //Label header = new Label
            //{
            //    Text = "Timing inside and out",
            //    Font = Font.BoldSystemFontOfSize(NamedSize.Large),
            //    HorizontalOptions = LayoutOptions.Center,
            //    TextColor=Xamarin.Forms.Color.White
            //};

            var subHeader = new Label
            {
                Text = "Context means timing all behavior, workflow, and tasks however granular or grand – not simply measuring page loads, referrals, and transactions.",
                TextColor = Color.Accent,
                Font = Font.SystemFontOfSize(NamedSize.Medium)


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
                PAClientFactory.StartFeature("User Task");
				_isFeaturePressed=true;
			};
			stopButton.Clicked += (s, e) => {
				stopButton.IsEnabled=false;
				startButton.IsEnabled=true;
                PAClientFactory.StopFeature("User Task");
				_isFeaturePressed=false;
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
                    
                  
                }
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    //header,
                    subHeader,
                    stackLayout
                }
            };
        }

        public override string Feature
        {
            get
            {
                return "View User Task";
            }
        }
    }
}
