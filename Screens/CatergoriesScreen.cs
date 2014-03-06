using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Todooy.Core;
using Todooy.ApplicationLayer;

namespace Todooy.Screens {

    /// <summary>
    /// A UITableViewController that uses MonoTouch.Dialog - displays the list of Categories
    /// </summary>
    public class CategoriesScreen : DialogViewController {
        // 
        List<Category> categories;
        
        // MonoTouch.Dialog individual CategoryDetails view (uses /AL/CategoryDialog.cs wrapper class)
        BindingContext context;
        CategoryDialog categoryDialog;
        Category currentCategory;
        DialogViewController detailsScreen;

        public CategoriesScreen () : base (UITableViewStyle.Plain, null)
        {
            Initialize ();
        }
        
        protected void Initialize()
        {
            NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
            NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowCategoryDetails(new Category()); };
        }
        
        protected void ShowCategoryDetails(Category category)
        {
            currentCategory = category;
            categoryDialog = new CategoryDialog (category);
            context = new BindingContext (this, categoryDialog, "Category Details");
            detailsScreen = new DialogViewController (context.Root, true);
            ActivateController(detailsScreen);
        }
        public void SaveCategory()
        {
            context.Fetch (); // re-populates with updated values
            currentCategory.Name = categoryDialog.Name;
            CategoryManager.SaveCategory(currentCategory);
            NavigationController.PopViewControllerAnimated (true);
        }
        public void DeleteCategory ()
        {
            if (currentCategory.ID >= 0)
                CategoryManager.DeleteCategory (currentCategory.ID);
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
                
            Root = new RootElement("Category   \t") {s};
            
            categories = CategoryManager.GetCategories().ToList();
            
            categories.ForEach(c => {
                s.Add(new StringElement (c.Name == "" ? "<new category>" : c.Name));
            });

        }
        public override void Selected (MonoTouch.Foundation.NSIndexPath indexPath)
        {
            var category = categories[indexPath.Row];
            ShowCategoryDetails(category);
        }
        public override Source CreateSizingSource (bool unevenRows)
        {
            return new CategorySource (this);
        }
        public void DeleteCategoryRow(int rowId)
        {
            CategoryManager.DeleteCategory(categories[rowId].ID);
        }
    }
}