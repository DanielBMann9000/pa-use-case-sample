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
using System.Threading.Tasks;
using PreEmptive.Analytics.Common;
using Xamarin.Forms;

namespace PASample
{
	class PerformancePage : BasePage
	{
		/*I don't like having the button state stored in a static but how do I share the state as the user goes from page to page*/
		private static bool _isFeaturePressed=false;

		public PerformancePage()
		{
			this.Title = "Timing inside and out";

			var startButton = new Button {
				Text = "Start",
				HorizontalOptions = LayoutOptions.Start,
				IsEnabled=!_isFeaturePressed


			};
			var stopButton = new Button {
				Text = "Stop",
				HorizontalOptions = LayoutOptions.Start,
				IsEnabled=_isFeaturePressed
			};
			startButton.Clicked += (s, e) => {
				stopButton.IsEnabled=true;
				startButton.IsEnabled=false;
				PAClientFactory.StartFeature("User Task");
				_isFeaturePressed=true;
			};
			stopButton.Clicked += (s, e) => {
				stopButton.IsEnabled=false;
				startButton.IsEnabled=true;
				PAClientFactory.StopFeature("User Task");
				_isFeaturePressed=false;
			};
			var goButon = new Button
			{

				Text = "Go",

			};
			goButon.Clicked += goButon_Clicked;


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
							startButton,
							stopButton,
													 
						}
					},
					new StackLayout
					{
						Children=
						{
							   new Label{Text="Click Me"},
							goButon
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
   
					this.ContextMeans(),
					stackLayout
				}
			};
		}

		async void  goButon_Clicked(object sender, EventArgs e)
		{
			try
			{
				var client = new System.Net.Http.HttpClient();
				var response = await client.GetAsync("http://192.168.10.124:84/api/values/" + PAClientFactory.GetPAClient().GetActiveDefaultSession().ToString());
				var result = await response.Content.ReadAsByteArrayAsync();

                var str=System.Text.Encoding.Unicode.GetString(result, 0, result.Length);
				await DisplayAlert("result",str,"");
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}

		public override string ContextMeansText
		{
			get
			{
				return " timing all behavior, workflow, and tasks however granular or grand – not simply measuring page loads, referrals, and transactions.";
			}
		}
		public override string Feature
		{
			get
			{
				return "View User Task";
			}
		}
	}
}
