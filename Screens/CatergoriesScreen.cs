using System.Collections.Generic;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using Todooy.ApplicationLayer;
using Todooy.Core;

namespace Todooy.Screens {


    public class CategoriesScreen : DialogViewController {

		public List<Category> Categories;
        
		BindingContext context;

		DialogViewController detailsScreen;

        Category currentCategory;

		UITableViewController tasksControllerView;

		public CategoriesScreen () : base (UITableViewStyle.Plain, null)
        {
            Initialize ();
        }
        
		protected void Initialize()
        {
			NavigationItem.SetLeftBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Edit), false);

			NavigationItem.LeftBarButtonItem.Clicked += (sender, e) => {
				if (base.TableView.Editing) {
					base.TableView.SetEditing(false, true); 
				} else {
					base.TableView.SetEditing(true, true);
				}
			};
		

            NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);

            NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowCategoryDetails(new Category()); };

			TableView.SeparatorInset = UIEdgeInsets.Zero;
        }

		public void ShowCategoryDetails(Category category)
        {
            currentCategory = category;

			context = new BindingContext (this, currentCategory, "New Category");

			detailsScreen = new DialogViewController (context.Root, true);

			ActivateController(detailsScreen);
        }

		protected void ShowCategoryTasks(Category category)
		{
			currentCategory = category;

			tasksControllerView = new Screens.TasksScreen (category);

			NavigationController.PushViewController(tasksControllerView, true);
		}

        public void SaveCategory()
        {
			context.Fetch();

            CategoryManager.SaveCategory(currentCategory);
            
			NavigationController.PopViewControllerAnimated (true);
		}

        public void DeleteCategory ()
        {
            if (currentCategory.Id >= 0)
                CategoryManager.DeleteCategory (currentCategory.Id);
            
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
                
			Root = new RootElement("Categories") {s};
            
			Categories = CategoryManager.GetCategories().ToList();
            
			Categories.ForEach(c => {
				StyledStringElement sse = new StyledStringElement (c.Name == "" ? "<new category>" : c.Name) {
					Accessory = UITableViewCellAccessory.DisclosureIndicator,
					TextColor = Category.CategoryColors[(int)c.Color]
				};

				s.Add(sse);
            });

        }


		public override void Selected (MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var category = Categories[indexPath.Row];

			ShowCategoryTasks(category);
		}


        public override Source CreateSizingSource (bool unevenRows)
        {
            return new CategorySource (this);
        }

        public void DeleteCategoryRow(int rowId)
        {
            CategoryManager.DeleteCategory(Categories[rowId].Id);
        }
    }
}