using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Todooy.Core;
using Todooy.ApplicationLayer;

namespace Todooy.Screens {

	public class TasksScreen : DialogViewController {

        public List<Task> Tasks;
		
		BindingContext context;

		Task currentTask;

		DialogViewController detailsScreen;

        Category currentCategory;

		public TasksScreen (Category category) : base (UITableViewStyle.Plain, null)
		{
			this.currentCategory = category;

			Initialize ();
		}
		
		protected void Initialize()
		{
			NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);

			NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowTaskDetails(new Task()); };

			TableView.SeparatorInset = UIEdgeInsets.Zero;
		}
		
		protected void ShowTaskDetails(Task task)
		{
			currentTask = task;

            currentTask.CategoryId = currentCategory.Id;

			context = new BindingContext (
				this, 
				currentTask, 
				task.Name == null ? "New Task" : task.Name
			);

			detailsScreen = new DialogViewController (context.Root, true);

			ActivateController(detailsScreen);
		}

		public void SaveTask()
		{
			context.Fetch ();

			TaskManager.SaveTask(currentTask);

			NavigationController.PopViewControllerAnimated (true);
		}

		public void DeleteTask ()
		{
            if (currentTask.Id >= 0)
                TaskManager.DeleteTask (currentTask.Id);

			NavigationController.PopViewControllerAnimated (true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			PopulateTable();

			NavigationItem.SetHidesBackButton (false, false);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			NavigationItem.SetHidesBackButton (true, false);
		}
		
		public void PopulateTable()
		{
			var s = new Section ();
				
			Root = new RootElement(currentCategory.Name) {s};

            Tasks = TaskManager.GetTasks(currentCategory.Id).ToList();

			Tasks.ForEach(t => {
                    
                StyledStringElement sse = new StyledStringElement (
                    string.IsNullOrEmpty(t.Name) ? "<New Task>" : t.Name, 
                    null, 
                    UITableViewCellStyle.Subtitle
                );

                if (t.Done) {
                    sse.Accessory = UITableViewCellAccessory.Checkmark;
                }

                if (t.DueDate) {
                    sse.Value = t.Date.ToShortDateString();

					if (t.Date.Date > DateTime.Today) {
						sse.DetailColor = UIColor.Green;
					} else if (t.Date.Date <= DateTime.Today) {
						sse.DetailColor = UIColor.Red;
                    }
                }

                s.Add(sse);
			});
		}

		public override void Selected (MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var task = Tasks[indexPath.Row];

			ShowTaskDetails(task);
		}

		public override Source CreateSizingSource (bool unevenRows)
		{
			return new TaskSource (this);
		}

		public void DeleteTaskRow(int rowId)
		{
            TaskManager.DeleteTask(Tasks[rowId].Id);
		}
	}
}