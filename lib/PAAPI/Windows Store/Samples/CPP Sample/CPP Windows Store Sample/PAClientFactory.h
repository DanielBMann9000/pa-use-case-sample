#pragma once

namespace CPP_Windows_Store_Sample
{
	ref class PAClientFactory sealed
	{
	public:
		static PreEmptive::Analytics::WinRT::PAClient^ GetPAClient();
	};
}