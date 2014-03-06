using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Todooy {
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {
		// class-level declarations
		UIWindow window;
		UINavigationController navController;
		UITableViewController tasksViewController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
			// create our nav controller
			navController = new UINavigationController ();

			// create our home controller based on the device
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				tasksViewController = new Screens.TasksScreen ();
			} else {
				// sample does not contain an iPad UI, so re-use the iPhone one for now; this just shows how you might support iPad
				tasksViewController = new Screens.TasksScreen ();
			}

			// push the view controller onto the nav controller and show the window
			navController.PushViewController(tasksViewController, false);
			window.RootViewController = navController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}