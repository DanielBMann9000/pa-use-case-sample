using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class PerformancePage : BasePage
    {
        public PerformancePage()
        {
            Label header = new Label
            {
				Text = "Timing inside and out",
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
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
							new Button
                            {
                                Text = "Start",
                                HorizontalOptions = LayoutOptions.Start
                            },
							new Button
                            {
                                Text = "Stop",
								HorizontalOptions=LayoutOptions.End
                            },
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
                    stackLayout
                }
            };
        }
    }
}
