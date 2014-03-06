using System;

namespace Todooy.Core {
    /// <summary>
    /// Category business object
    /// </summary>
    public class Category {
        public Category ()
        {
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}