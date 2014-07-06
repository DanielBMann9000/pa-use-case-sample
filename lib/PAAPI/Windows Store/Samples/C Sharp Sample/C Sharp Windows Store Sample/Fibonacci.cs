using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.WindowsStore;

namespace C_Sharp_Windows_Store_Sample
{
    public static class Fibonacci
    {
        private static PAClient client;

        static Fibonacci()
        {
            client = PAClientFactory.GetPAClient();
        }

        public static void CalculateFibonacci(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException();
            }

            // Use PAClient to record how long it takes to calculate the value.
            // We track the value of n as part of the feature message.
            var keys = new ExtendedKeys();
            keys.Add("Value of n", n);

            // We use a variable to hold the feature name so we make sure our start/stop use the same name.
            // This can be important if you use a dynamic feature name like "fibonacci(15)".
            string featureName = string.Format("Calculate Fibonacci of {0}", n);

            client.FeatureStart(featureName, keys);

            int fib = 0;

            if (n == 0 || n == 1)
            {
                fib = n;
            }
            else
            {
                int a = 0;
                int b = 1;

                for (int i = 2; i <= n; ++i)
                {
                    fib = a + b;
                    a = b;
                    b = fib;
                }
            }

            // Tell the client that our calculation has ended.
            // Add the result to our keys: "Value of n" is still in there from above.
            keys.Add("Resulting Fibonacci Value", fib);
            client.FeatureStop(featureName, keys);
        }
    }
}
