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
	class AboutPage : BasePage
    {
        public AboutPage()
        {
            this.Title = "PA Sample App";

           
            Label label = new Label
            {
                
                Text ="The PA Sample App lets users select preferences, time their actions, and throw different types of exceptions inside the app. All of this “user and application behavior” including the specific features exercised during each session are tracked using PreEmptive Analytics instrumentation. Additionally, a “user identity” consisting of a role and a user key is synthesized on a session-by-session basis – you can see your value at any time by visiting the Identity page. NO actual PII is ever collected. ",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor=Xamarin.Forms.Color.White
            };
            Label label2 = new Label
            {
                Text ="The resulting telemetry is transmitted to a PreEmptive Analytics™ endpoint where it is ingested and processed using the PreEmptive Analytics Workbench and PreEmptive Analytics for TFS.",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Xamarin.Forms.Color.White
            };
            Label label3 = new Label
            {
                Text =  "This application is a working sample of the kinds of analytics patterns that can be captured and was developed using Visual Studio, C#, Xamarin, and (of course) PreEmptive Analytics.  To download this project including source code, documentation, and access to the analytics endpoints mentioned above, email sampleanalyticsapp@preemptive.com and instructions will be provided. For more information on PreEmptive Analytics (on and beyond mobile), visit www.preemptive.com/pa.",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Xamarin.Forms.Color.White
            };
            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new ScrollView
            {

                Content = new StackLayout
                 {
                     Children = 
                    {
                       
                        label,
                        label2,
                        label3
                    }
                 }
            };
        }
        

        public override string Feature
        {
            get
            {
                return "About";
            }
        }
    }
}
