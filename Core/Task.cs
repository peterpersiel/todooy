using System;
using MonoTouch.UIKit;
using Todooy.Core;
using MonoTouch.Dialog;
using Todooy.ApplicationLayer;

namespace Todooy.Core {

	public class Task {
        public Task () {}

		public Task (Task task)
		{
            Id          = task.Id;
            Name        = task.Name;
            Notes       = task.Notes;
            Done        = task.Done;
            Date        = task.Date;
            CategoryId = task.CategoryId;
		}

        public int Id;

        public int CategoryId;

        [Entry("Task Name")]
        public string Name;

		[Entry("Notes")]
        public string Notes;

		[Entry("Done")]
        [Checkbox]
        public bool Done;

        [Section ("")]
        [Caption("Due Date")]
        public bool DueDate;

        [Caption("")]
		[Date]
		public DateTime Date = DateTime.Now;

		[Section ("")]
		[OnTap ("SaveTask")]
		[Alignment (UITextAlignment.Center)]
		public string Save;

		[Section ("")]
		[OnTap ("DeleteTask")]
		[Alignment (UITextAlignment.Center)]
		public string Delete;
	}
}