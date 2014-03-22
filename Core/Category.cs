using System.Collections.Generic;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace Todooy.Core {

    public enum CategoryColor {
        Red,
        Green,
        Blue,
        Black,
        Orange,
        Brown,
        Cyan,
        Magenta,
        Purple
    }

    public class Category {

        public Category (Category category) {
            Color = category.Color;
            Id    = category.Id;
            Name  = category.Name;
        }

        public Category () {}

        public static IList<UIColor> CategoryColors = new List<UIColor> {
        	MonoTouch.UIKit.UIColor.Red,
        	MonoTouch.UIKit.UIColor.Green,
        	MonoTouch.UIKit.UIColor.Blue,
        	MonoTouch.UIKit.UIColor.Black,
        	MonoTouch.UIKit.UIColor.Orange,
        	MonoTouch.UIKit.UIColor.Brown,
        	MonoTouch.UIKit.UIColor.Cyan,
        	MonoTouch.UIKit.UIColor.Magenta,
        	MonoTouch.UIKit.UIColor.Purple
        };

        public int Id;

        [Entry("Category Name")]
        public string Name;

        [Caption("Color")]
        public CategoryColor Color;

        [Section ("")]
        [OnTap ("SaveCategory")]
        [Alignment (UITextAlignment.Center)]
        public string Save;
    }
}