//
// MainPage.xaml.cpp
// Implementation of the MainPage class.
//

#include "pch.h"
#include "MainPage.xaml.h"
#include "Fibonacci.h"
#include "PAClientFactory.h"

using namespace CPP_Windows_Store_Sample;

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;

MainPage::MainPage()
{
	InitializeComponent();
}

/// <summary>
/// Invoked when this page is about to be displayed in a Frame.
/// </summary>
/// <param name="e">Event data that describes how this page was reached.  The Parameter
/// property is typically used to configure the page.</param>
void MainPage::OnNavigatedTo(NavigationEventArgs^ e)
{
	(void) e;	// Unused parameter
}

void MainPage::RunSample(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e) 
{
	// Optional RI step: Generate a system profile message so we know something about the execution environment.
	PAClientFactory::GetPAClient()->SystemProfile();

	for (int n = -1; n < 20; ++n) 
	{
		try 
		{
			Fibonacci::CalculateFibonacci(n);
		}
		catch(Platform::Exception^ e)
		{
			PAClientFactory::GetPAClient()->ReportException(PreEmptive::Analytics::WinRT::ExceptionInfo::Caught(e->GetType()->FullName, e->Message));
		}
	}
}

void MainPage::ThrowUnhandledException(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e) 
{
	throw Platform::Exception::CreateException(100,"Unhandled Exception");
}
