using System;

namespace Todooy.Core {
	/// <summary>
	/// Task business object
	/// </summary>
	public class Task {
		public Task ()
		{
		}

        public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
        public int CategoryId { get; set; }
	}
}