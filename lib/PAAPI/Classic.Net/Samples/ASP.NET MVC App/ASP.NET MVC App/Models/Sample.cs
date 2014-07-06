using System;
using PreEmptive.Analytics.Common;

namespace ASP.NET_MVC_App.Models
{
    public class Sample
    {
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
            throw new Exception("This exception is unhandled.");
        }
    }
}