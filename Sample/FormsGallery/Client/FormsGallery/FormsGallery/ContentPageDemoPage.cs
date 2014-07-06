using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class BasePageDemoPage : BasePage
    {
        public BasePageDemoPage()
        {
            Label header = new Label
            {
                Text = "BasePage",
                Font = Font.BoldSystemFontOfSize(40),
                HorizontalOptions = LayoutOptions.Center
            };

            Label label1 = new Label
            {
                Text = "BasePage is the simplest type of page.",
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };

            Label label2 = new Label
            {
                Text = "The content of a BasePage is generally a " +
                       "layout of some sort that can then be a parent " +
                       "to multiple children.",
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };

            Label label3 = new Label
            {
                Text = "This BasePage contains a StackLayout, which " +
                       "in turn contains four Label views (including the " +
                       "large one at the top)",
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    label1,
                    label2,
                    label3
                }
            };



        }
    }
}
