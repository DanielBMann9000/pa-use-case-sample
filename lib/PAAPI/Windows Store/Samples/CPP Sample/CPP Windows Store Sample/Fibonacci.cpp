#include "pch.h"
#include "Fibonacci.h"
#include "PAClientFactory.h"

using namespace Platform;
using namespace CPP_Windows_Store_Sample;

void Fibonacci::CalculateFibonacci(int n)
{
    if (n < 0)
    {
		throw Platform::Exception::CreateException(1,"The number cannot be negative");
    }

    // Use the client to record how long it takes to calculate the value.
    // We track the value of n as part of the feature message.
    auto keys = ref new Platform::Collections::Map<String^, Object^>;
	keys->Insert("Value of n", n);

    // We use a variable to hold the feature name so we make sure our start/stop use the same name.
    // This can be important if you use a dynamic feature name like "fibonacci(15)".
    String^ featureName = "Calculate Fibonacci of " + n;
	
    PAClientFactory::GetPAClient()->FeatureStart(featureName, keys);

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
	keys->Insert("Resulting Fibonacci Value", fib);
    PAClientFactory::GetPAClient()->FeatureStop(featureName, keys);
}