using System;
using Xamarin.Forms;

namespace PASample
{
    class ExceptionPage : BasePage
    {
        private Random _rand = new Random();
        public ExceptionPage()
        
        {
            this.Title = "Throw exceptions – It’s fun!";
            //Label header = new Label
            //{
            //    Text = "Throw exceptions – It’s fun!",
            //    Font = Font.BoldSystemFontOfSize(NamedSize.Large),
            //    HorizontalOptions = LayoutOptions.Center,
            //    TextColor = Xamarin.Forms.Color.White
            //};

            var subHeader = new Label
            {
                Text = "Context means distinguishing between application, runtime, and user issues and knowing how to prioritize them all.",
                TextColor = Color.Accent,
                Font = Font.SystemFontOfSize(NamedSize.Medium)


            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Button handled = new Button
            {
                Text = "Handled",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,

            };

            Button unhandled = new Button
            {
                Text = "Unhandled",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Button thrown = new Button
            {
                Text = "Thrown",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    //header,
                    subHeader,
					handled,
                    thrown,
					unhandled
                }
            };

            handled.Clicked += OnHandledClicked;
            unhandled.Clicked += OnUnhandledClicked;
            thrown.Clicked += thrown_Clicked;
        }
        public override string Feature
        {
            get
            {
                return "Incident Alert";
            }
        }
        void thrown_Clicked(object sender, EventArgs e)
        {
            var ex = new System.ArgumentException("Argument is incorrect");
            PAClientFactory.Exception(ex, PreEmptive.Analytics.Common.ExceptionType.Thrown);
            DisplayAlert("Error", "Thrown Error simulated and sent to PA", "Ok", null);
        }
        void OnUnhandledClicked(object sender, EventArgs e)
        {
            var ex=new NotImplementedException("This is an unhandled excption");
            PAClientFactory.Exception(ex, PreEmptive.Analytics.Common.ExceptionType.Uncaught);
            DisplayAlert("Error", "Unhandled Error simulated and sent to PA", "Ok", null);
         


        }
        void OnHandledClicked(object sender, EventArgs e)
        {
            try
            {
                var val = throwException(0);
            }
            catch (Exception ex)
            {
                PAClientFactory.Exception(ex, PreEmptive.Analytics.Common.ExceptionType.Caught);
                DisplayAlert("Error", ex.Message, "Ok", null);
            }


        }

        decimal throwException(decimal val)
        {
            switch (_rand.Next(0, 2))
            {

                case 0:
                    return 10 / val;
                case 1:
                    object a = null;
                    a.GetType();
                    break;
                case 2:
                    var b = "myString";
                    return b.LastIndexOf('m', 10);

            }
            return 0;
        }
    }
}
