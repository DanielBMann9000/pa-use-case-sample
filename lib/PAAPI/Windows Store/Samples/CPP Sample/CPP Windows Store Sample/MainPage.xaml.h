//
// MainPage.xaml.h
// Declaration of the MainPage class.
//

#pragma once

#include "MainPage.g.h"

namespace CPP_Windows_Store_Sample
{
	public ref class MainPage sealed
	{
	public:
		MainPage();
		property PreEmptive::Analytics::WinRT::PAClient^ client;
	protected:
		virtual void OnNavigatedTo(Windows::UI::Xaml::Navigation::NavigationEventArgs^ e) override;
		void RunSample(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);
		void ThrowUnhandledException(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);
	};
}
