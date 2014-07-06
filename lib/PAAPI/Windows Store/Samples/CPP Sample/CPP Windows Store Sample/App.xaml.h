//
// App.xaml.h
// Declaration of the App class.
//

#pragma once

#include "App.g.h"

namespace CPP_Windows_Store_Sample
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	ref class App sealed
	{
	public:
		App();
		virtual void OnLaunched(Windows::ApplicationModel::Activation::LaunchActivatedEventArgs^ args) override;

	private:	
		void OnSuspending(Platform::Object^ sender, Windows::ApplicationModel::SuspendingEventArgs^ e);
		void OnException(Platform::Object^ sender, Windows::UI::Xaml::UnhandledExceptionEventArgs^ args);
		void OnResume(Platform::Object^ sender, Platform::Object^ args);
		PreEmptive::Analytics::WinRT::FlowController^ flowController;
		PreEmptive::Analytics::WinRT::BinaryInfo^ binaryInfo;
	};
}
