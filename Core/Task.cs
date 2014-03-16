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
            this.Id          = task.Id;
            this.Name        = task.Name;
            this.Notes       = task.Notes;
            this.Done        = task.Done;
            this.Date        = task.Date;
            this.CategoryId  = task.CategoryId;
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