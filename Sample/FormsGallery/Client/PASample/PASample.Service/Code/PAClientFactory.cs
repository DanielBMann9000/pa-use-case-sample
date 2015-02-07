using System;
// Copyright (c) 2014 PreEmptive Solutions; All Right Reserved, http://www.preemptive.com/
//
// This source is subject to the Microsoft Public License (MS-PL).
// Please see the License.txt file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.NET;


namespace PASample.Service
{
	public static class PAClientFactory
	{
		private static PAClient Client;

		public static void StopApplication(bool immediate=false)
		{
			GetPAClient ().ApplicationStop (immediate:immediate);
		}
		public static void StartApplication()
		{

	
			GetPAClient().ApplicationStart();
		}
		public static void FeatureTick(Guid sessionId,string name)
		{
			GetPAClient ().FeatureTick(name,session:sessionId);
		}
		public static void FeatureTick(string name, ExtendedKeys keys)
		{
			GetPAClient().FeatureTick(name, keys);
		}
		public static void StartFeature(string name)
		{
			GetPAClient ().FeatureStart (name);
		}
		public static void StopFeature(string name, ExtendedKeys keys=null)
		{
			GetPAClient ().FeatureStop (name,keys);
		}
		public static void Exception(System.Exception ex,ExceptionType exType)
		{
			GetPAClient ().ReportException (new ExceptionInfo {
				Comment="user comment",
				Contact="TODO:Get UserId",
				Exception=ex,
				ExceptionType=exType,
				Message=ex.Message
			});
		}
		public static void Exception(System.Exception ex)
		{
			Exception (ex, ExceptionType.Uncaught);
		}



		private static PAClient GetPAClient()
		{
			if (Client == null)
			{
				Client = GetPAClient(null);
			}
			return Client;
		}
		public static PAClient GetPAClient(string instance=null,string username=null)
		{
			if (Client == null)
			{
				// Required step: create the configuration structure and populate it
				// This tells the server who is sending the messages via the company and application IDs.
				// There are lots of other configuration settings that control the content and sending of messages.
				// The first GUID is the company ID provided by PreEmptive Solutions.
				// The second Guid is provided by you to identify the application.
				// This is the endpoint described here http://www.preemptive.com/support/resources/ris-ce
				
				

				var configuration = new Configuration("1d2b02e0-064d-49a0-bc1b-4be4381c62d3", "42AC2020-ABA1-9069-A2BD-98072B33309A");

	   
				configuration.ApplicationName = "PA Sample Service";
				configuration.ApplicationType = "Sample";
				configuration.CompanyName = "PreEmptive Solutions";
				configuration.InstanceID = instance;
				configuration.ApplicationVersion = "1.2";
				configuration.Endpoint = "so-s.info/endpoint";
				//configuration.Endpoint = "josh-2012r2-2.preemptive.internal/endpoint";
				configuration.UseSSL = false;
				configuration.FullData = true;
				configuration.StopBehavior.SessionExtensionWindow = 15000;
				configuration.SupportOfflineStorage = true;
                

				Client = new PAClient(configuration);
				


			}

			return Client;
		}

	}
}
