﻿using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class ExceptionPage : BasePage
    {
        private Random _rand = new Random();
        public ExceptionPage()
        {
            Label header = new Label
            {
                Text = "Throw exceptions – It’s fun!",
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
            };



            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Button handled = new Button
            {
                Text = "Handled",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Button unhandled = new Button
            {
                Text = "Unhandled",
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
                    header,
					handled,
					unhandled
                }
            };

            handled.Clicked += OnHandledClicked;
            unhandled.Clicked += OnUnhandledClicked;
        }
        void OnUnhandledClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException("This is an unhandled excption");


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
