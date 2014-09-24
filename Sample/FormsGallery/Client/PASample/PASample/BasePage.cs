using System;
using Xamarin.Forms;

namespace PASample
{
    public class BasePage : ContentPage
    {
        public virtual string Feature
        {
            get
            {
                return this.ToString();
            }
        }
        public BasePage()
        {
            this.BackgroundColor = Color.Black;
         
        }
        protected override void OnAppearing()
        {
            PAClientFactory.StartFeature(this.Feature);
        }
        protected override void OnDisappearing()
        {
            PAClientFactory.StopFeature(this.Feature);
        }
        protected Label ContextMeans()
        {
            Label subHeader = new Label
            {
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span{
                            Text="Context means",
                            Font=Font.SystemFontOfSize(NamedSize.Medium,FontAttributes.Bold)
                        },
                        new Span{
                            Text=this.ContextMeansText
                        }
                    }
                },
                
                TextColor = Color.Accent,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BackgroundColor=Color.Black
            };
            return subHeader;
        }


        public virtual string ContextMeansText
        {
            get { return string.Empty; }
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

