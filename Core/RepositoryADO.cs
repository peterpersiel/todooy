using System;
using System.Collections.Generic;
using System.IO;

namespace Todooy.Core {
	public class RepositoryADO {
		DatabaseADO db = null;
		protected static string DbLocation;		
		protected static RepositoryADO Me;		

		static RepositoryADO ()
		{
			Me = new RepositoryADO();
		}

		protected RepositoryADO ()
		{
			DbLocation = DatabaseFilePath;

			db = new DatabaseADO(DbLocation);
		}

		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "todooy.db3";
				
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				string libraryPath = Path.Combine (documentsPath, "..", "Library");

				var path = Path.Combine (libraryPath, sqliteFilename);

				return path;	
			}
		}

		public static Task GetTask(int id)
		{
			return Me.db.GetTask(id);
		}

		public static IEnumerable<Task> GetTasks (int categoryId)
		{
			return Me.db.GetTasks(categoryId);
		}

		public static int SaveTask (Task item)
		{
			return Me.db.SaveTask(item);
		}

		public static int DeleteTask(int id)
		{
			return Me.db.DeleteTask(id);
		}
		
		public static Category GetCategory(int id)
		{
			return Me.db.GetCategory(id);
		}

		public static IEnumerable<Category> GetCategories ()
		{
			return Me.db.GetCategories();
		}

		public static int SaveCategory (Category item)
		{
			return Me.db.SaveCategory(item);
		}

		public static int DeleteCategory(int id)
		{
			return Me.db.DeleteCategory(id);
		}
	}
}

