using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Todooy.Core;
using Todooy.ApplicationLayer;

namespace Todooy.Screens {

	/// <summary>
	/// A UITableViewController that uses MonoTouch.Dialog - displays the list of Tasks
	/// </summary>
	public class HomeScreen : DialogViewController {
		// 
		List<Task> tasks;
		
		// MonoTouch.Dialog individual TaskDetails view (uses /AL/TaskDialog.cs wrapper class)
		BindingContext context;
		TaskDialog taskDialog;
		Task currentTask;
		DialogViewController detailsScreen;

		public HomeScreen () : base (UITableViewStyle.Plain, null)
		{
			Initialize ();
		}
		
		protected void Initialize()
		{
			NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowTaskDetails(new Task()); };
		}
		
		protected void ShowTaskDetails(Task task)
		{
			currentTask = task;
			taskDialog = new TaskDialog (task);
			context = new BindingContext (this, taskDialog, "Task Details");
			detailsScreen = new DialogViewController (context.Root, true);
			ActivateController(detailsScreen);
		}
		public void SaveTask()
		{
			context.Fetch (); // re-populates with updated values
			currentTask.Name = taskDialog.Name;
			currentTask.Notes = taskDialog.Notes;
			currentTask.Done = taskDialog.Done;
			TaskManager.SaveTask(currentTask);
			NavigationController.PopViewControllerAnimated (true);
		}
		public void DeleteTask ()
		{
			if (currentTask.ID >= 0)
				TaskManager.DeleteTask (currentTask.ID);
			NavigationController.PopViewControllerAnimated (true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			// reload/refresh
			PopulateTable();			
		}
		
		protected void PopulateTable()
		{
			var s = new Section ();
				
			Root = new RootElement("Tasks") {s};
			
			tasks = TaskManager.GetTasks().ToList();
			
			tasks.ForEach(t => {
				s.Add(new CheckboxElement ((t.Name == "" ? "<new task>" : t.Name), t.Done));
            });
			
			
		}
		public override void Selected (MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var task = tasks[indexPath.Row];
			ShowTaskDetails(task);
		}
		public override Source CreateSizingSource (bool unevenRows)
		{
			return new EditingSource (this);
		}
		public void DeleteTaskRow(int rowId)
		{
			TaskManager.DeleteTask(tasks[rowId].ID);
		}
	}
}