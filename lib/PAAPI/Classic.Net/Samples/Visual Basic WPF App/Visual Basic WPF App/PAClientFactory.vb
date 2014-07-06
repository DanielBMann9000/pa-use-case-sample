Imports PreEmptive.Analytics.NET
Imports PreEmptive.Analytics.Common

Public Class PAClientFactory
    Private Shared client As PAClient

    Public Shared Function GetPAClient() As PAClient
        If client Is Nothing Then
            'Required step: create the configuration structure and populate it
            'This tells the server who is sending the messages via the company and application IDs.
            'There are lots of other configuration settings that control the content and sending of messages.
            'The first GUID is the company ID provided by PreEmptive Solutions.
            'The second Guid is provided by you to identify the application.
            'This is the endpoint described here http://www.preemptive.com/support/resources/ris-ce
            Dim config As New Configuration("7d2b02e0-064d-49a0-bc1b-4be4381c62d3", "42AC2020-ABA1-9069-A2BD-98072B33309A")

            'Optional configuration
            config.CompanyName = "PreEmptive Solutions"
            config.ApplicationName = "VB Sample App"
            config.ApplicationType = "VB Sample"
            config.ApplicationVersion = "1.0"
            config.Endpoint = "message.runtimeintelligence.com/PreEmptive.Web.Services.Messaging/MessagingServiceV2.asmx"
            config.UseSSL = False
            config.FullData = False
            'config.SupportOfflineStorage = True
            'config.OmitPersonalInfo = True

            client = New PAClient(config)
        End If

        GetPAClient = client
    End Function
End Class
