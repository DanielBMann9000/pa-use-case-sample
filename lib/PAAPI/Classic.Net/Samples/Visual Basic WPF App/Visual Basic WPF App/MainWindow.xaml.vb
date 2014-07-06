Imports PreEmptive.Analytics.NET
Imports PreEmptive.Analytics.Common

Class MainWindow
    Public Function RunSample_OnClick(sender As Object, events As RoutedEventArgs)
        Dim client As PAClient = PAClientFactory.GetPAClient
        Dim mw As MainWindow = New MainWindow()

        'Optional RI step: Generate a system profile message so we know something about the execution environment.
        client.SystemProfile()

        For n As Integer = -1 To 20
            Try
                Fibonacci.CalculateFibonacci(n)
            Catch ex As ArgumentException
                client.ReportException(ExceptionInfo.Caught(ex))
            End Try
        Next
    End Function

    Public Function ThrowException_OnClick(sender As Object, events As RoutedEventArgs)
        Throw New Exception()
    End Function
End Class

