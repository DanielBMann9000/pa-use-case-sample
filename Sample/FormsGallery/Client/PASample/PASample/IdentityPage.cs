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
                TextColor = Xamarin.Forms.Color.White,
				Font = Font.SystemFontOfSize(NamedSize.Large,FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center
            };
            


            TableView tableView = new TableView
            {
                BackgroundColor=Color.Black,
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
						  
                        new TextCell
                        {
                            Text = "License Key",TextColor=Xamarin.Forms.Color.White,
                            Detail = App.LicenseKey,
                            
                        },
						new TextCell{
							Text="Department",TextColor=Xamarin.Forms.Color.White,
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
