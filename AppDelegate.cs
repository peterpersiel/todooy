using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Todooy {
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {

		UIWindow window;

		UINavigationController navController;

        UITableViewController categoriesViewController;


		public override bool FinishedLaunching (UIApplication app, NSDictionary options) {
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			navController = new UINavigationController ();

            categoriesViewController = new Screens.CategoriesScreen ();

			navController.PushViewController(categoriesViewController, true);

			window.RootViewController = navController;

			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}