using System;
using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.WindowsStore;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace C_Sharp_Windows_Store_Sample
{
    sealed partial class App : Application
    {
        private PAClient client;
        private FlowController flowController;
        private BinaryInfo binaryInfo;

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
            Resuming += OnResuming;
            UnhandledException += OnUnhandledException;
            client = PAClientFactory.GetPAClient();

            // Optional step: Configure the behavior of the message queue.
            flowController = new FlowController();
            flowController.QueueSize = 100;
            flowController.HighWater = 50;
            //flowController.SendDisabled = true;


            // The use of the binary info and all of its fields are optional.
            binaryInfo = new BinaryInfo();
            binaryInfo.MethodName = "AppStart";
            binaryInfo.ClassName = "MainPage";
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;

                client.ApplicationStart(null, binaryInfo, flowController);

                //If you want to use the default values this is the call you can use. Default values are documented in the user guide.
                //client.ApplicationStart();
            }

            if (rootFrame.Content == null)
            {
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            Window.Current.Activate();
        }

        private void OnResuming(object sender, object e)
        {
            client.ApplicationStart(null, binaryInfo, flowController);

            //If you want to use the default values this is the call you can use. Default values are documented in the user guide.
            //client.ApplicationStart();
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();

            client.ApplicationStop();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            client.ReportException(ExceptionInfo.Uncaught(args.Exception));

            // If an unhandled exception will cause the application to terminate, you should call ApplicationStop.
            client.ApplicationStop();

            args.Handled = false;

            // If an unhandled exception shouldn't terminate the application, you can set handled to true.
            // However, if you do this, then you should not call ApplicationStop
            // args.Handled = true;
        }
    }
}
