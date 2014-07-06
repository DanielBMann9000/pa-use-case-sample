Imports PreEmptive.Analytics.NET
Imports PreEmptive.Analytics.Common

Public Class Fibonacci
    Public Shared Function CalculateFibonacci(n As Integer)
        If n < 0 Then
            Throw New ArgumentException()
        End If

        Dim client As PAClient = PAClientFactory.GetPAClient()

        'Use PAClient to record how long it takes to calculate the value.
        'We track the value of n as part of the feature message.
        Dim keys As ExtendedKeys = New ExtendedKeys()
        keys.Add("Value of n", n)

        'We use a variable to hold the feature name so we make sure our start/stop use the same name.
        'This can be important if you use a dynamic feature name like "fibonacci(15)".
        Dim featureName As String = String.Format("Calculate Fibonacci of {0}", n)

        client.FeatureStart(featureName, keys)

        Dim fib As Integer = 0

        If n = 0 Or n = 1 Then
            fib = n
        Else
            Dim a As Integer = 0
            Dim b As Integer = 1

            For i As Integer = 2 To n
                fib = a + b
                a = b
                b = fib
            Next
        End If

        'Tell the client that our calculation has ended.
        'Add the result to our keys: "Value of n" is still in there from above.
        keys.Add("Resulting Fibonacci Value", fib)
        client.FeatureStop(featureName, keys)
    End Function
End Class
