using System;
using System.Windows;
using Microsoft.Phone.Controls;
using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.WinPhone8;

namespace WP8_Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
        private PAClient client;

        public MainPage()
        {
            InitializeComponent();

            client = PAClientFactory.GetPAClient();
        }

        private void RunSample_OnClick(object sender, RoutedEventArgs e)
        {
            // Optional setp: Generate some performance info about the application
            client.PerformanceProbe("At Start");

            // Optional step: Generate a system profile message so we know something about the execution environment.
            client.SystemProfile();

            for (int n = -1; n < 20; ++n)
            {
                try
                {
                    Fibonacci.CalculateFibonacci(n);
                }
                catch (ArgumentException exception)
                {
                    client.ReportException(new ExceptionInfo(ExceptionType.Caught, exception));
                }
            }
        }

        private void ThrowUnhandledException_OnClick(object sender, RoutedEventArgs e)
        {
            throw new Exception("Unhandled Exception");
        }
    }
}