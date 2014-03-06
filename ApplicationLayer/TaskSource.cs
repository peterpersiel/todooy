using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using Todooy.Core;

namespace Todooy.ApplicationLayer {

	/// <summary>
	/// This is our subclass of the fixed-size Source that allows editing
	/// </summary>
	public class TaskSource : DialogViewController.Source {
		public TaskSource (DialogViewController dvc) : base (dvc) {}
		
		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			// Trivial implementation: we let all rows be editable, regardless of section or row
			return true;
		}
		
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			// trivial implementation: show a delete button always
			return UITableViewCellEditingStyle.Delete;
		}
		
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			//
			// In this method, we need to actually carry out the request
			//
			var section = Container.Root [indexPath.Section];
			var element = section [indexPath.Row] as StringElement;
			section.Remove (element);

			var dvc = Container as Screens.TasksScreen;
			dvc.DeleteTaskRow (indexPath.Row);
		}
	}
}