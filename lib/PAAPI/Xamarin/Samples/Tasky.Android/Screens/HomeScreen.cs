using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using PreEmptive.Analytics.Common;
using Tasky.BL;
using Android.Graphics;
using Android.Views;
using Tasky.Core;

namespace Tasky.Droid.Screens
{
    [Activity(Label = "TaskyPro", MainLauncher = true, Icon = "@drawable/launcher")]
    public class HomeScreen : Activity
    {
        protected Adapters.TaskListAdapter taskList;
        protected IList<Task> tasks;
        protected Button addTaskButton = null;
        protected ListView taskListView = null;

        protected override void OnCreate(Bundle bundle)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledExceptionHandler;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidUnhandledExceptionHandler;

            base.OnCreate(bundle);

            View titleView = Window.FindViewById(Android.Resource.Id.Title);
            if (titleView != null)
            {
                IViewParent parent = titleView.Parent;
                if (parent != null && (parent is View))
                {
                    View parentView = (View)parent;
                    parentView.SetBackgroundColor(Color.Rgb(0x26, 0x75, 0xFF)); //38, 117 ,255
                }
            }


            // set our layout to be the home screen
            SetContentView(Resource.Layout.HomeScreen);

            //Find our controls
            taskListView = FindViewById<ListView>(Resource.Id.lstTasks);
            addTaskButton = FindViewById<Button>(Resource.Id.btnAddTask);

            // wire up add task button handler
            if (addTaskButton != null)
            {
                addTaskButton.Click += (sender, e) =>
                {
                    StartActivity(typeof(TaskDetailsScreen));
                };
            }

            // wire up task click handler
            if (taskListView != null)
            {
                taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                {
                    var taskDetails = new Intent(this, typeof(TaskDetailsScreen));
                    taskDetails.PutExtra("TaskID", tasks[e.Position].ID);
                    StartActivity(taskDetails);
                };
            }
        }

        protected override void OnStart()
        {
            PAClientFactory.GetPAClient().ApplicationStart();
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();

            PAClientFactory.GetPAClient().ApplicationStop();
        }

        protected override void OnResume()
        {
            base.OnResume();

            tasks = Tasky.BL.Managers.TaskManager.GetTasks();

            // create our adapter
            taskList = new Adapters.TaskListAdapter(this, tasks);

            //Hook up our adapter to our ListView
            taskListView.Adapter = taskList;
        }

        private static void AndroidUnhandledExceptionHandler(object sender, RaiseThrowableEventArgs args)
        {
            ReportException(args.Exception);
        }

        private static void CurrentDomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            ReportException((Exception)e.ExceptionObject);
        }

        private static void ReportException(Exception e)
        {
            var client = PAClientFactory.GetPAClient();

            client.ReportException(ExceptionInfo.Uncaught(e));
            // If an unhandled exception will cause the application to terminate, you should call ApplicationStop.
            client.ApplicationStopSync(immediate: true);
        }
    }
}