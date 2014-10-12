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
            //this.BackgroundColor = Color.Black;
         
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
                
                TextColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Medium)//,
                //BackgroundColor=Color.Black
            };
            return subHeader;
        }


        public virtual string ContextMeansText
        {
            get { return string.Empty; }
        }
    }
	
}

