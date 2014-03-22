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
            UIColor.Red,
            UIColor.Green,
            UIColor.Blue,
            UIColor.Black,
            UIColor.Orange,
            UIColor.Brown,
            UIColor.Cyan,
            UIColor.Magenta,
            UIColor.Purple
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