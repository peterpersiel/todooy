using System;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Todooy.ApplicationLayer {

    public class CategorySource : DialogViewController.Source {
        public CategorySource (DialogViewController dvc) : base (dvc) {}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}
			
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var dvc = Container as Screens.CategoriesScreen;

			switch (editingStyle) {
                case UITableViewCellEditingStyle.Delete:

                    var section = Container.Root[indexPath.Section];
                    var element = section[indexPath.Row] as StringElement;

                    section.Remove(element);

                    if (dvc != null)
                    {
                        dvc.DeleteCategoryRow(indexPath.Row);
                    }

					break;

				case UITableViewCellEditingStyle.None:
					Console.WriteLine ("CommitEditingStyle:None called");
					break;
			}
		}
    }
}