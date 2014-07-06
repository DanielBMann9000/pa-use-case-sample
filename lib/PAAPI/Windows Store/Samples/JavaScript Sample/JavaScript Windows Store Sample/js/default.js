var analytics = PreEmptive.Analytics.WinRT;
var client;
var configuration;
var flowController;
var binaryInfo;

(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;
    WinJS.Application.onerror = OnError;
    
    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                
            } else {
                
            }
            
            if (client == null) {
                // Required step: create the configuration structure and populate it
                // This tells the server who is sending the messages via the company and application IDs.
                // There are lots of other configuration settings that control the content and sending of messages.
                // The first GUID is the company ID provided by PreEmptive Solutions.
                // The second Guid is provided by you to identify the application.
                // This is the endpoint described here http://www.preemptive.com/support/resources/ris-ce
                var configuration = new analytics.Configuration("7d2b02e0-064d-49a0-bc1b-4be4381c62d3", "42AC2020-ABA1-9069-A2BD-98072B33309A");

                // Optional configuration
                configuration.companyName = "PreEmptive Solutions";
                configuration.applicationName = "JavaScript Sample App";
                configuration.applicationType = "JavaScript Sample";
                configuration.applicationVersion = "1.0";
                configuration.endpoint = "message.runtimeintelligence.com/PreEmptive.Web.Services.Messaging/MessagingServiceV2.asmx";
                configuration.useSSL = false;
                configuration.fullData = false;
                //configuration.supportOfflineStorage = true;
                //configuration.omitPersonalInfo = true;

                client = new analytics.PAClient(configuration);

                // Optional step: Configure the behavior of the message queue.
                flowController = new analytics.FlowController();
                flowController.queueSize = 100;
                flowController.highWater = 50;
                //flowController.sendDisabled = true;


                // The use of the binary info and all of its fields are optional.
                binaryInfo = new analytics.BinaryInfo();
                binaryInfo.methodName = "appStart";
                binaryInfo.className = "default";
            }

            client.applicationStart(null, binaryInfo, flowController);
            
            //If you want to use the default values this is the call you can use. Default values are documented in the user guide.
            //client.ApplicationStart();
            
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        client.applicationStop();
    };

    app.start();
})();

function throwError() {
    throw new Error("Unhandled Error");
}

function runSample() {
    // Optional step: Generate a system profile message so we know something about the execution environment.
    client.systemProfile();
    
    for (var i = -1; i < 20; ++i) {
        try {
            fibonacci(i);
        }
        catch (error) {
            client.reportException(analytics.ExceptionInfo.caught(error.name, error.message, error.stack));
        }
    }
}

function fibonacci(n) {
    if (n < 0) {
        throw new Error("Invalid Argument");
    }

    // Use the client to record how long it takes to calculate the value.
    // We track the value of n as part of the feature message.
    var keys = new Windows.Foundation.Collections.PropertySet();
    keys["Value of n"] = n;

    // We use a variable to hold the feature name so we make sure our start/stop use the same name.
    // This can be important if you use a dynamic feature name like "fibonacci(15)".
    var featureName = "Calculate Fibonacci of " + n;

    client.featureStart(featureName, keys);

    var fib = 0;
    
    if (n == 0 || n == 1) {
        fib = n;
    } else {
        var a = 0;
        var b = 1;
        
        for (var i = 2; i <= n; ++i) {
            fib = a + b;
            a = b;
            b = fib;
        }
    }

    // Tell the client that our calculation has ended.
    // Add the result to our keys: "Value of n" is still in there from above.
    keys["Resulting Fibonacci Value"] = fib;
    client.featureStop(featureName, keys);
}

function OnError(e) {
    client.reportException(analytics.ExceptionInfo.uncaught(new Error(e.detail.error.type, e.detail.error.message)));
    
    // If an unhandled error will cause the application to terminate, you should call applicationStop.
    client.applicationStop();

    return false;

    // If an unhandled exception shouldn't terminate the application, you can return true, to signal it was handled.
    // However, if you do this, then you should not call ApplicationStop
    //return true;
}
