using System;
using System.Collections.Generic;

namespace Todooy.Core {
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public static class TaskManager {
		static TaskManager ()
		{

		}
		
		public static Task GetTask(int id)
		{
			return RepositoryADO.GetTask(id);
		}
		
		public static IList<Task> GetTasks (int categoryId)
		{
			return new List<Task>(RepositoryADO.GetTasks(categoryId));
		}
		
		public static int SaveTask (Task item)
		{
			return RepositoryADO.SaveTask(item);
		}
		
		public static int DeleteTask(int id)
		{
			return RepositoryADO.DeleteTask(id);
		}
	}
}