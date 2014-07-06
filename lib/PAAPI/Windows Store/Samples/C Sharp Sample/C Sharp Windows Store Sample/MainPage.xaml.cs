using System;
using System.Collections.Generic;
using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.WindowsStore;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace C_Sharp_Windows_Store_Sample
{
    public sealed partial class MainPage : Page
    {
        private PAClient client;
        public MainPage()
        {
            InitializeComponent();
            client = PAClientFactory.GetPAClient();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void RunSample_OnClick(object sender, RoutedEventArgs e)
        {
            // Optional RI step: Generate a system profile message so we know something about the execution environment.
            client.SystemProfile();

            for (int n = -1; n < 20; ++n)
            {
                try
                {
                    Fibonacci.CalculateFibonacci(n);
                }
                catch (ArgumentException exception)
                {   
                    client.ReportException(ExceptionInfo.Caught(exception));
                }
            }
        }

        private void ThrowException_OnClick(object sender, RoutedEventArgs e)
        {
            throw new ArgumentNullException();
        }
    }
}
