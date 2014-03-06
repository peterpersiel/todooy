using System;
using System.Collections.Generic;
using System.IO;

namespace Todooy.Core {
	public class RepositoryADO {
		DatabaseADO db = null;
		protected static string dbLocation;		
		protected static RepositoryADO me;		

		static RepositoryADO ()
		{
			me = new RepositoryADO();
		}

		protected RepositoryADO ()
		{
			// set the db location
			dbLocation = DatabaseFilePath;

			// instantiate the database	
			db = new DatabaseADO(dbLocation);
		}

		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "TaskDatabase.db3";
				#if NETFX_CORE
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
				#else

				#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = sqliteFilename;
				#else

				#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
				#endif

				#endif
				return path;	
			}
		}

		public static Task GetTask(int id)
		{
			return me.db.GetTask(id);
		}

		public static IEnumerable<Task> GetTasks (int categoryId)
		{
			return me.db.GetTasks(categoryId);
		}

		public static int SaveTask (Task item)
		{
			return me.db.SaveTask(item);
		}

		public static int DeleteTask(int id)
		{
			return me.db.DeleteTask(id);
		}
		
		public static Category GetCategory(int id)
		{
			return me.db.GetCategory(id);
		}

        public static IEnumerable<Category> GetCategories ()
		{
			return me.db.GetCategories();
		}

		public static int SaveCategory (Category item)
		{
			return me.db.SaveCategory(item);
		}

		public static int DeleteCategory(int id)
		{
			return me.db.DeleteCategory(id);
		}
	}
}

