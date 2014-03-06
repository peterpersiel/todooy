using System;
using System.Linq;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.IO;
using System.Data;

namespace Todooy.Core
{
	/// <summary>
	/// TaskDatabase uses ADO.NET to create the tables and create,read,update,delete data
	/// </summary>
	public class DatabaseADO 
	{
		static object locker = new object ();

		public SqliteConnection connection;

		public string path;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		public DatabaseADO (string dbPath) 
		{
			var output = "";
			path = dbPath;
			// create the tables
			bool exists = File.Exists (dbPath);

			if (!exists) {
				connection = new SqliteConnection ("Data Source=" + dbPath);

				connection.Open ();
				var commands = new[] {
					"CREATE TABLE [Tasks] (_id INTEGER PRIMARY KEY ASC, CategoryId INTEGER, Name NTEXT, Notes NTEXT, Done INTEGER, FOREIGN KEY (CategoryId) REFERENCES Categories(_id));",
					"CREATE TABLE [Categories] (_id INTEGER PRIMARY KEY ASC, Name NTEXT);"
				};
				foreach (var command in commands) {
					using (var c = connection.CreateCommand ()) {
						c.CommandText = command;
						var i = c.ExecuteNonQuery ();
					}
				}
			} else {
				// already exists, do nothing. 
			}
			Console.WriteLine (output);
		}
		
		/// <summary>Convert from DataReader to Category object</summary>
		Category CategoryReader (SqliteDataReader r) {
			var t = new Category ();
			t.ID = Convert.ToInt32 (r ["_id"]);
			t.Name = r ["Name"].ToString ();
			return t;
		}

		public IEnumerable<Task> GetCategories ()
		{
			var tl = new List<Category> ();

			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var contents = connection.CreateCommand ()) {
					contents.CommandText = "SELECT [_id], [Name] from [Categories]";
					var r = contents.ExecuteReader ();
					while (r.Read ()) {
						tl.Add (CategoryReader(r));
					}
				}
				connection.Close ();
			}
			return tl;
		}

		public Task GetCategory (int id) 
		{
			var t = new Category ();
			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "SELECT [_id], [Name] from [Categories] WHERE [_id] = ?";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });
					var r = command.ExecuteReader ();
					while (r.Read ()) {
						t = CategoryReader (r);
						break;
					}
				}
				connection.Close ();
			}
			return t;
		}

		public int SaveCategory (Category item) 
		{
			int r;
			lock (locker) {
				if (item.ID != 0) {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "UPDATE [Categories] SET [Name] = ? WHERE [_id] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				} else {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [Categories] ([Name]) VALUES (?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				}

			}
		}

		public int DeleteCategory(int id) 
		{
			lock (locker) {
				int r;
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "DELETE FROM [Categories] WHERE [_id] = ?;";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
					r = command.ExecuteNonQuery ();
				}
				connection.Close ();
				return r;
			}
		}

		/// <summary>Convert from DataReader to Task object</summary>
		Task TaskReader (SqliteDataReader r) {
			var t = new Task ();
			t.ID = Convert.ToInt32 (r ["_id"]);
			t.Name = r ["Name"].ToString ();
			t.Notes = r ["Notes"].ToString ();
			t.Done = Convert.ToInt32 (r ["Done"]) == 1 ? true : false;
			t.CategoryId = t.Done = Convert.ToInt32 (r ["CategoryId"]) == 1 ? true : false;
			return t;
		}

		public IEnumerable<Task> GetTasks (int categoryId)
		{
			var tl = new List<Task> ();

			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var contents = connection.CreateCommand ()) {
					contents.CommandText = "SELECT [_id], [Name], [Notes], [Done] from [Tasks] WHERE [CategoryId] = ?";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = categoryId });
					var r = contents.ExecuteReader ();
					while (r.Read ()) {
						tl.Add (TaskReader(r));
					}
				}
				connection.Close ();
			}
			return tl;
		}

		public Task GetTask (int id) 
		{
			var t = new Task ();
			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "SELECT [_id], [Name], [Notes], [Done] from [Items] WHERE [_id] = ?";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });
					var r = command.ExecuteReader ();
					while (r.Read ()) {
						t = TaskReader (r);
						break;
					}
				}
				connection.Close ();
			}
			return t;
		}

		public int SaveTask (Task item) 
		{
			int r;
			lock (locker) {
				if (item.ID != 0) {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "UPDATE [Tasks] SET [Name] = ?, [Notes] = ?, [Done] = ?, [CategoryId] = ? WHERE [_id] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Notes });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Done });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.CategoryId });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.ID });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				} else {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [Tasks] ([Name], [Notes], [Done], [CategoryId]) VALUES (? ,?, ?, ?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Notes });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Done });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.CategoryId });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				}

			}
		}

		public int DeleteTask(int id) 
		{
			lock (locker) {
				int r;
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "DELETE FROM [Tasks] WHERE [_id] = ?;";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
					r = command.ExecuteNonQuery ();
				}
				connection.Close ();
				return r;
			}
		}
	}
}