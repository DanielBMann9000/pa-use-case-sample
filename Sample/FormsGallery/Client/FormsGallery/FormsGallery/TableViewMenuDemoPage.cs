﻿using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class TableViewMenuDemoPage : BasePage
    {
        public TableViewMenuDemoPage()
        {
            Label header = new Label
            {
                Text = "TableView for a menu",
                Font = Font.BoldSystemFontOfSize(30),
                HorizontalOptions = LayoutOptions.Center
            };

            TableView tableView = new TableView
                {
                    Intent = TableIntent.Menu,
                    Root = new TableRoot
                    {
                        new TableSection("Views for Presentation")
                        {
                            new TextCell
                            {
                                Text = "Label",
                                Command = new Command(async () => 
                                    await Navigation.PushAsync(new AboutPage()))
                            },

                            new TextCell
                            {
                                Text = "Image",
                                Command = new Command(async () => 
                                    await Navigation.PushAsync(new ImageDemoPage()))
                            },

                            new TextCell
                            {
                                Text = "BoxView",
                                Command = new Command(async () => 
								await Navigation.PushAsync(new ExceptionPage()))
                            },

                            new TextCell
                            {
                                Text = "WebView",
                                Command = new Command(async () => 
                                    await Navigation.PushAsync(new WebViewDemoPage()))
                            },
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
                    header,
                    tableView
                }
            };
        }
    }
}
