//
// App.xaml.cpp
// Implementation of the App class.
//

#include "pch.h"
#include "MainPage.xaml.h"
#include "PAClientFactory.h"

using namespace CPP_Windows_Store_Sample;

using namespace Platform;
using namespace Windows::ApplicationModel;
using namespace Windows::ApplicationModel::Activation;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Interop;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;

App::App()
{
	InitializeComponent();

	Suspending += ref new SuspendingEventHandler(this, &App::OnSuspending);
	UnhandledException += ref new UnhandledExceptionEventHandler(this, &App::OnException);
	Resuming += ref new EventHandler<Platform::Object^>(this, &App::OnResume);
}

void App::OnLaunched(Windows::ApplicationModel::Activation::LaunchActivatedEventArgs^ args)
{
	auto rootFrame = dynamic_cast<Frame^>(Window::Current->Content);

	if (rootFrame == nullptr)
	{
		rootFrame = ref new Frame();

		if (args->PreviousExecutionState == ApplicationExecutionState::Terminated)
		{
		}

		if (rootFrame->Content == nullptr)
		{
			if (!rootFrame->Navigate(TypeName(MainPage::typeid), args->Arguments))
			{
				throw ref new FailureException("Failed to create initial page");
			}
		}

		Window::Current->Content = rootFrame;
		Window::Current->Activate();
	}
	else
	{
		if (rootFrame->Content == nullptr)
		{
			if (!rootFrame->Navigate(TypeName(MainPage::typeid), args->Arguments))
			{
				throw ref new FailureException("Failed to create initial page");
			}
		}

		Window::Current->Activate();
	}

	// Optional step: Configure the behavior of the message queue.
	flowController = ref new PreEmptive::Analytics::WinRT::FlowController();
	flowController->QueueSize = 100;
	flowController->HighWater = 50;
	//flowController->SendDisabled = true;

	// The use of the binary info and all of its fields are optional.
	binaryInfo = ref new PreEmptive::Analytics::WinRT::BinaryInfo();
    binaryInfo->MethodName = "AppStart";
    binaryInfo->ClassName = "MainPage";

	PAClientFactory::GetPAClient()->ApplicationStart(nullptr, binaryInfo, flowController);

	//If you want to use the default values this is the call you can use. Default values are documented in the user guide.
	//PAClientFactory::GetPAClient()->ApplicationStart();
}

void App::OnResume(Platform::Object^ sender, Platform::Object^ args) 
{
	PAClientFactory::GetPAClient()->ApplicationStart(nullptr, binaryInfo, flowController);
}

/// <summary>
/// Invoked when application execution is being suspended.  Application state is saved
/// without knowing whether the application will be terminated or resumed with the contents
/// of memory still intact.
/// </summary>
/// <param name="sender">The source of the suspend request.</param>
/// <param name="e">Details about the suspend request.</param>
void App::OnSuspending(Object^ sender, SuspendingEventArgs^ e)
{
	(void) sender;	// Unused parameter
	(void) e;	// Unused parameter

	PAClientFactory::GetPAClient()->ApplicationStop();
}

void App::OnException(Platform::Object^ sender, Windows::UI::Xaml::UnhandledExceptionEventArgs^ args) {
	PAClientFactory::GetPAClient()->ReportException(PreEmptive::Analytics::WinRT::ExceptionInfo::Uncaught(args->Exception));

	// If an unhandled error will cause the application to terminate, you should call applicationStop.
	PAClientFactory::GetPAClient()->ApplicationStop();

	args->Handled = false;

    // If an unhandled exception shouldn't terminate the application, you can set handled to true.
    // However, if you do this, then you should not call ApplicationStop
	// args->Handled = true;
}
