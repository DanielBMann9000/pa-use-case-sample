using System;
using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.NET;

namespace C_Sharp_Console_App
{
    public class MainClass
    {
        public static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += MyHandler;

            var client = PAClientFactory.GetPAClient();

            // Optional step: Configure the behavior of the message queue.
            var flowController = new FlowController();
            flowController.QueueSize = 100;
            flowController.HighWater = 50;
            //flowController.SendDisabled = true;


            // The use of the binary info and all of its fields are optional.
            var binaryInfo = new BinaryInfo();
            binaryInfo.MethodName = "AppStart";
            binaryInfo.ClassName = "MainPage";

            client.ApplicationStart(null, binaryInfo, flowController);

            // If you want to use the default values this is the call you can use. Default values are documented in the user guide.
            // client.ApplicationStart();

            Console.WriteLine("PreEmptive Solutions");
            Console.WriteLine("C# Console Application Sample");

            Console.WriteLine();

            int selection = 0;

            while (selection != 3)
            {
                Console.WriteLine();
                Console.WriteLine("1: Run Sample");
                Console.WriteLine("2: Throw Unhandled Exception");
                Console.WriteLine("3: Exit");

                if (int.TryParse(Console.ReadLine(), out selection))
                {
                    switch (selection)
                    {
                        case 1:
                            RunSample();
                            break;
                        case 2:
                            ThrowUnhandledException();
                            break;
                    }
                }
            }

            client.ApplicationStop();
        }

        public static void RunSample()
        {
            var client = PAClientFactory.GetPAClient();

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

        public static void ThrowUnhandledException()
        {
            throw new Exception();
        }

        public static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var client = PAClientFactory.GetPAClient();

            Exception e = (Exception)args.ExceptionObject;
            client.ReportException(ExceptionInfo.Uncaught(e));
            // If an unhandled exception will cause the application to terminate, you should call ApplicationStop.
            client.ApplicationStop();
        }
    }
}
