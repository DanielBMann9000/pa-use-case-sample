using System;
using System.Collections.Generic;
using Tasky.BL;
using Tasky.Core;

namespace Tasky.BL.Managers
{
	public static class TaskManager
	{
		static TaskManager ()
		{
		}
		
		public static Task GetTask(int id)
		{
#if Android || iOS
            PAClientFactory.GetPAClient().FeatureTick("GetTask");
#endif

			return DAL.TaskRepository.GetTask(id);
		}
		
		public static IList<Task> GetTasks ()
		{
#if Android || iOS
            PAClientFactory.GetPAClient().FeatureTick("GetTasks");
#endif

			return new List<Task>(DAL.TaskRepository.GetTasks());
		}
		
		public static int SaveTask (Task item)
		{
#if Android || iOS
            PAClientFactory.GetPAClient().FeatureTick("SaveTask");
#endif
            // Added by PreEmptive Solutions as a way of easily triggering an unhandled exception to test analytics.
            if (item.Name.ToUpper() == "FAIL")
            {
                throw new Exception("An intentionally thrown unhandled exception.");
            }

			return DAL.TaskRepository.SaveTask(item);
		}
		
		public static int DeleteTask(int id)
		{
#if Android || iOS
            PAClientFactory.GetPAClient().FeatureTick("DeleteTask");
#endif

			return DAL.TaskRepository.DeleteTask(id);
		}
	}
}