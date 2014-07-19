using System;
using Xamarin.Forms;

namespace FormsGallery
{
	class AboutPage : BasePage
    {
        public AboutPage()
        {
            Label header = new Label
            {
				Text = "PreEmptive Analytics Use Case Gallery ",
				Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
            };

            Label label = new Label
            {
                Text =
					"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lacus turpis, sollicitudin quis rhoncus non, ornare ac ante. Nullam luctus dapibus pellentesque. Curabitur dapibus diam ut vestibulum posuere. Nunc ornare adipiscing arcu id accumsan. Integer fringilla mi at mi faucibus, in ullamcorper justo consectetur. Curabitur id luctus ante. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vivamus ac pellentesque velit. Maecenas egestas elit et consectetur ullamcorper. Maecenas rhoncus fermentum enim non eleifend. " +
					"Curabitur ultricies nisl nulla. Quisque lacinia quam eget ultrices dignissim. Ut dictum nisi ligula, a fermentum lacus ultricies vitae. Aenean at mattis lorem, sit amet tempus mi.",

                Font = Font.SystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    label
                }
            };
        }
    }
}
