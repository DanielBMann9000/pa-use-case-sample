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

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using System.Threading.Tasks;

namespace PASample.Droid
{
    [Activity(Label = "PA Sample App", MainLauncher = true)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);


            SetPage(App.GetMainPage());
          
			AndroidEnvironment.UnhandledExceptionRaiser += (s, e) => {
				PAClientFactory.Exception(e.Exception);
			};
        }
      
        protected override void OnStart()
        {
            base.OnStart();
            App.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            App.Shutdown();
        }




    }


}

