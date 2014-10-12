// Copyright (c) 2014 PreEmptive Solutions; All Right Reserved, http://www.preemptive.com/
//
// This source is subject to the Microsoft Public License (MS-PL).
// Please see the License.txt file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using Xamarin.Forms;

namespace PASample
{
    class IdentityPage : BasePage
    {
        public IdentityPage()
        {
            this.Title = "Session ID";
            Label header = new Label
            {
				Text = "This is your ID for the duration of this session",
				Font = Font.SystemFontOfSize(NamedSize.Large,FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center
            };
            


            TableView tableView = new TableView
            {
                //BackgroundColor=Color.Black,
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
							Text="Department",
							Detail=App.Department
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
                    this.ContextMeans(),
                    tableView
                }
            };
        }

        public override string Feature
        {
            get
            {
                return "View Profile";
            }
        }
        public override string ContextMeansText
        {
            get
            {
                return " users have personas and identities – they are more than sessions, page views, and clicks.";
            }
   
        }
    }
}
