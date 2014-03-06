using System;
using MonoTouch.UIKit;
using Todooy.Core;
using MonoTouch.Dialog;

namespace Todooy.ApplicationLayer {
    /// <summary>
    /// Wrapper class for Category, to use with MonoTouch.Dialog. If it was just iOS platform
    /// we could apply these attributes directly to the Category class, but because we share that
    /// with other platforms this wrapper provides a bridge to MonoTouch.Dialog.
    /// </summary>
    public class CategoryDialog {
        public CategoryDialog (Category category)
        {
            Name  = category.Name;
        }
        
        [Entry("category name")]
        public string Name { get; set; }

        
        [Section ("")]
        [OnTap ("SaveCategory")]
        [Alignment (UITextAlignment.Center)]
        public string Save;
        
        [Section ("")]
        [OnTap ("DeleteCategory")]
        [Alignment (UITextAlignment.Center)]
        public string Delete;
    }
}