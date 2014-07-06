﻿using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class TextCellDemoPage : BasePage
    {
        public TextCellDemoPage()
        {
            Label header = new Label
            {
                Text = "TextCell",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };

            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        new TextCell
                        {
                            Text = "This is a TextCell",
                            Detail = "This is some detail text",
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
                    header,
                    tableView
                }
            };
        }
    }
}
