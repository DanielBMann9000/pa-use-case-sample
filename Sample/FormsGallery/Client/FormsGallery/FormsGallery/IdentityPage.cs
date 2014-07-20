﻿using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class IdentityPage : BasePage
    {
        public IdentityPage()
        {
            Label header = new Label
            {
				Text = "This is your ID for the duration of this session",
				Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
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
                            Text = "License Key",
                            Detail = App.LicenseKey,
                        },
						new TextCell{
							Text="User Name",
							Detail=App.UserName
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
