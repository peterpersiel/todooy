AdvancedColorPicker
===================
An open source color picker component for Xamarin.iOS

Usage
=====
AdvancedColorPicker is very simple and easy to use:

1. Include AdvancedColorPicker in your solution and add a reference to it.
2. Create an instance of ColorPickerViewController
3. Show ColorPickerViewController.View
4. Subscribe to ColorPickerViewController.ColorPicked event, to be notified when the user picks a color and
   examine the value of ColorPickerViewController.SelectedColor property that holds the color the user selected

You can also use the ColorPickerViewController.SelectedColor property to get/set the selected color.   
When you change the value of this property and the ColorPickerViewController.View is visible, the color
selection indicators will pick the color using an animation.


```csharp
using AdvancedColorPicker;
...

ColorPickerViewController picker;

void pickAColorBtn_HandleTouchUpInside (object sender, EventArgs e)
{
	picker = new ColorPickerViewController();
	picker.ColorPicked += HandleColorPicked;
	picker.Title = "Pick a color!";
	UINavigationController pickerNav = new UINavigationController(picker);
	pickerNav.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
	NavigationController.PresentModalViewController(pickerNav,true);
}

//This event is raised every time a color is selected
void HandleColorPicked ()
{
	MyView.BackgroundColor = picker.SelectedColor;
}

```


Take a look at the AdvancedColorPickerDemo project about how to use it.



Compatibility
==============
AdvancedColorPicker is tested on iOS 4.3+, 5.0+, 6.0+, both on iPhone and iPad.

All devices, screen sizes and orientations are supported because AdvancedColorPicker 
does not use images neither nib files, but custom drawing and dynamic views creation to display everything.