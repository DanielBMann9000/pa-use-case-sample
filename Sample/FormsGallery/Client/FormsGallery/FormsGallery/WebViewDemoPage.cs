﻿using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class WebViewDemoPage : BasePage
    {
        public WebViewDemoPage()
        {
            Label header = new Label
            {
                Text = "WebView",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };

            PAClientFactory.StartFeature("ServiceLevel");
            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = "http://www.preemptive.com/pa"

                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                
            };


            
            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    webView
                }
            };
        }
    }
}
