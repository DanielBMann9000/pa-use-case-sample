using System;
using System.Collections.Generic;
using PreEmptive.Analytics.Common;
using Xamarin.Forms;

namespace FormsGallery
{
    class FeedbackPage : BasePage
    {
        Label labelSlider;
        // Dictionary to get Color from color name.
        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua },         { "Black", Color.Black },
            { "Blue", Color.Blue },         { "Fuschia", Color.Fuschia },
            { "Gray", Color.Gray },         { "Green", Color.Green },
            { "Lime", Color.Lime },         { "Maroon", Color.Maroon },
            { "Navy", Color.Navy },         { "Olive", Color.Olive },
            { "Purple", Color.Purple },     { "Red", Color.Red },
            { "Silver", Color.Silver },     { "Teal", Color.Teal },
            { "White", Color.White },       { "Yellow", Color.Yellow }
        };

        public FeedbackPage()
        {
            Label header = new Label
            {
                Text = "Feedback & Preferences",
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
            };


            Label headerSlider = new Label
            {
                Text = "Happiness Index",
                Font = Font.BoldSystemFontOfSize(NamedSize.Medium),
                HorizontalOptions = LayoutOptions.Center
            };

            Slider slider = new Slider
            {
                Minimum = 0,
                Maximum = 10,
                Value=5,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            slider.ValueChanged += OnSliderValueChanged;

            labelSlider = new Label
            {
                Text = "Your happiness value is 0",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };




            Picker picker = new Picker
            {
                Title = "Favorite Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            BoxView boxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker.SelectedIndexChanged += (sender, args) =>
                {
                    if (picker.SelectedIndex == -1)
                    {
                        boxView.Color = Color.Default;
                    }
                    else
                    {
                        string colorName = picker.Items[picker.SelectedIndex];
                        boxView.Color = nameToColor[colorName];
                    }
                };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            var button = new Button()
            {
                Text="Submit"
            };
            button.Clicked += (sender, args) =>
            {
                var keys=new ExtendedKeys();
                keys.Add("Happiness",slider.Value);
                keys.Add("Color",picker.Items[picker.SelectedIndex]);
                PAClientFactory.FeatureTick("Feedback",keys);
                
                DisplayAlert("Feedback Accepted", "Thank you for submitting your feedback.", "Ok","Cancel");

            };
            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    header,
                    headerSlider,
                    slider,
                    labelSlider,
                    picker,
                    boxView,
                    button
                    
                }
            };

        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            labelSlider.Text = String.Format("Your Happiness value is {0:F1}", e.NewValue);
        }
    }
}
