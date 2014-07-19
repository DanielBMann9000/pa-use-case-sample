﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsGallery
{
    public class App
    {
        private static Random _rand = new Random();

        private static string[] _keys = { "ABC-123", "XYZ-432", "EFG-234", "HIJ-012", "YUI-765", "ITI-912", "LKL-876", "TRE-432", "LIK-101", "TRE-111" };
        private static string[] _userNames = { "Josh", "Fred", "Nathan", "Bill", "Mark", "Gabe", "Joe", "Pat", "Cindy", "Laura", "Emily" };

        private static string _userName = _userNames[_rand.Next(0, _userNames.Length)];
        private static string _licenseKey = GetLicenseKey();
        public static Page GetMainPage()
        {
			
            return new NavigationPage(new HomePage());
        }

		public static void Shutdown()
		{
			PAClientFactory.StopApplication (true);
		}

        public static void Start()
        {
            
            PAClientFactory.StartApplication(_licenseKey);

        }



        public static string GetLicenseKey()
        {
            return _keys[_rand.Next(0, _keys.Length - 1)];

        }

        public static string LicenseKey
        {
            get
            {
                return _licenseKey;
            }
        }

        public static string UserName
        {
            get
            {
                return _userName;
            }
        }
    }
}
