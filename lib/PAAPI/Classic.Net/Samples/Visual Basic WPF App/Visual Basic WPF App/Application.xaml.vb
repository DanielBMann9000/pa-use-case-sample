Imports PreEmptive.Analytics.NET
Imports PreEmptive.Analytics.Common

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.
    Private Function App_Startup(sender As Object, e As StartupEventArgs)
        'AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf ExceptionHandler

        'Optional step: Configure the behavior of the message queue.
        Dim flowController As FlowController = New FlowController()
        flowController.QueueSize = 100
        flowController.HighWater = 50
        'flowController.SendDisabled = True

        'The use of the binary info and all of its fields are optional.
        Dim binaryInfo As BinaryInfo = New BinaryInfo()
        binaryInfo.MethodName = "AppStart"
        binaryInfo.ClassName = "Application"

        Dim client As PAClient = PAClientFactory.GetPAClient()
        client.ApplicationStart(Nothing, binaryInfo, flowController)

        'If you want to use the default values this is the call you can use. Default values are documented in the user guide.
        'client.ApplicationStart()
    End Function

    Private Function App_Stop(sender As Object, e As ExitEventArgs)
        Dim client As PAClient = PAClientFactory.GetPAClient()
        client.ApplicationStop()
    End Function

    Public Function UnhandledExceptionHandler(sender As Object, e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)
        Dim client As PAClient = PAClientFactory.GetPAClient()
        client.ReportException(ExceptionInfo.Uncaught(e.Exception))
        'If an unhandled exception will cause the application to terminate, you should call ApplicationStop.
        client.ApplicationStop()

        e.Handled = False
        'If an unhandled exception shouldn't terminate the application, you can set handled to true.
        'However, if you do this, then you should not call ApplicationStop
        'e.Handled = true;
    End Function
End Class
