using System;
using System.Collections.Generic;
using PreEmptive.Analytics.Common;
using Xamarin.Forms;

namespace PASample
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
            this.Title = "Feedback & Preferences";
            //Label header = new Label
            //{
            //    Text = "Feedback & Preferences",
            //    Font = Font.BoldSystemFontOfSize(NamedSize.Large),
            //    HorizontalOptions = LayoutOptions.Center,
            //    TextColor=Xamarin.Forms.Color.White
            //};

     
            Label headerSlider = new Label
            {
                Text = "Happiness Index",
                Font = Font.SystemFontOfSize(NamedSize.Large,FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center,
                TextColor=Xamarin.Forms.Color.White
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
               
                Text = "Your happiness value is 5",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Xamarin.Forms.Color.White
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
                if (picker.SelectedIndex >= 0)
                {
                    var keys = new ExtendedKeys();
                    keys.Add("Happiness", slider.Value);
                    keys.Add("Color", picker.Items[picker.SelectedIndex]);
                    PAClientFactory.FeatureTick("Feedback Submitted", keys);

                    DisplayAlert("Feedback Accepted", "Thank you for submitting your feedback.", "Ok", string.Empty);
                }
                else
                {
                    DisplayAlert("Error", "Please Select your favorite color.", "Ok", string.Empty);
                }
            };
            // Build the page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    //header,
                    this.ContextMeans(),
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

        public override string Feature
        {
            get
            {
                return "Feedback Request";
            }
        }
        public override string ContextMeansText
        {
            get
            {
                return " capturing user experience and satisfaction through direct feedback and behavioral analysis over time – not clicks, views, and likes.";
            }
        }
    }
    
}
