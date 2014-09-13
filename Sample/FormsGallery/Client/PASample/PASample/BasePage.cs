using System;
using Xamarin.Forms;

namespace PASample
{
	public class BasePage  : ContentPage
	{
        public virtual string Feature
        {
            get
            {
                return this.ToString();
            }
        }
		public BasePage () 
		{
        }
		protected override void OnAppearing ()
		{
			PAClientFactory.StartFeature (this.Feature);
		}
		protected override void OnDisappearing ()
		{
			PAClientFactory.StopFeature (this.Feature);
		}
	}
	public class BaseCarouselPage	  : CarouselPage
	{
        public virtual string Feature
        {
            get
            {
                return this.ToString();
            }
        }
		public BaseCarouselPage () 
		{

		}
		protected override void OnAppearing ()
		{
			PAClientFactory.StartFeature (this.Feature);
		}
		protected override void OnDisappearing ()
		{
			PAClientFactory.StopFeature (this.Feature);
		}
	}
}

