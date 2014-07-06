using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.WinPhone8;
using WP8_Sample.Resources;

namespace WP8_Sample
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        private PAClient client;
        private FlowController flowController;
        private BinaryInfo binaryInfo;

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;
            UnhandledException += OnUnhandledException;

            InitializeComponent();
            InitializePhoneApplication();
            InitializeLanguage();

            client = PAClientFactory.GetPAClient();

            if (Debugger.IsAttached)
            {
                Application.Current.Host.Settings.EnableFrameRateCounter = true;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            // Optional step: Configure the behavior of the message queue.
            flowController = new FlowController();
            flowController.QueueSize = 100;
            flowController.HighWater = 50;
            //flowController.SendDisabled = true;


            // The use of the binary info and all of its fields are optional.
            binaryInfo = new BinaryInfo();
            binaryInfo.MethodName = "AppStart";
            binaryInfo.ClassName = "App";
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            client.ApplicationStart(null, binaryInfo, flowController);

            //If you want to use the default values this is the call you can use. Default values are documented in the user guide.
            //client.ApplicationStart();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            client.ApplicationStart(null, binaryInfo, flowController);

            //If you want to use the default values this is the call you can use. Default values are documented in the user guide.
            //client.ApplicationStart();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            client.ApplicationStop();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            client.ApplicationStop();
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        #region Phone application initialization

        private bool phoneApplicationInitialized = false;

        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            RootFrame.Navigated += CheckForResetNavigation;

            phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            RootFrame.Navigated -= ClearBackStackAfterReset;

            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            while (RootFrame.RemoveBackEntry() != null)
            {

            }
        }

        #endregion

        private void InitializeLanguage()
        {
            try
            {
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }

        private void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            client.ReportException(ExceptionInfo.Uncaught(e.ExceptionObject));

            // If an unhandled exception will cause the application to terminate, you should call ApplicationStop.
            client.ApplicationStop();

            e.Handled = false;

            // If an unhandled exception shouldn't terminate the application, you can set handled to true.
            // However, if you do this, then you should not call ApplicationStop
            // e.Handled = true;
        }
    }
}