﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FormsGallery
{
    public class App
    {
        public static Page GetMainPage()
        {
			PAClientFactory.StartApplication ("MyInstance");
            return new NavigationPage(new HomePage());
        }
    }
}
