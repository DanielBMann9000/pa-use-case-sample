using System;
using Xamarin.Forms;

namespace FormsGallery
{
	public class BasePage  : ContentPage
	{
		public BasePage () 
		{

		}
		protected override void OnAppearing ()
		{
			PAClientFactory.StartFeature (this.ToString());
		}
		protected override void OnDisappearing ()
		{
			PAClientFactory.StopFeature (this.ToString());
		}
	}
}

