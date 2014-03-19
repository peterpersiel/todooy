using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Mono.Data.Sqlite;
using Todooy.Extensions;

namespace Todooy.Core
{
	public class DatabaseADO 
	{
		static object locker = new object ();

		public SqliteConnection Connection;

		public string Path;

		public DatabaseADO (string dbPath) 
		{
			Path = dbPath;

            bool exists = File.Exists (dbPath);

			if (!exists) {
				Connection = new SqliteConnection ("Data Source=" + dbPath);

				Connection.Open ();

				var commands = new[] {
                    "CREATE TABLE [Category] (" +
						"Id INTEGER NOT NULL PRIMARY KEY ASC, " +
						"Name NTEXT, " +
						"Color INTEGER" +
					");",
					"CREATE TABLE [Task] (" +
						"Id INTEGER NOT NULL PRIMARY KEY ASC," +
						"Name NTEXT, " +
						"Notes NTEXT, " +
						"Done INTEGER, " +
                        "Date INTEGER, " +
                        "CategoryId INTEGER," +
                       "FOREIGN KEY (CategoryId) REFERENCES Category(Id)" +
					");",
				};

				foreach (var command in commands) {
					using (var c = Connection.CreateCommand ()) {
						c.CommandText = command;
						c.ExecuteNonQuery ();

						Console.WriteLine (command);
					}
				}

			}
		}
		
		Category CategoryReader (SqliteDataReader r) {
			var c = new Category ();

            c.Id    = Convert.ToInt32 (r ["Id"]);
			c.Name  = r ["Name"].ToString ();
			c.Color = (CategoryColor)Convert.ToInt32 (r ["Color"]);

			return c;
		}

		public IEnumerable<Category> GetCategories ()
		{
			var tl = new List<Category> ();

			lock (locker) {

				Connection = new SqliteConnection ("Data Source=" + Path);

				Connection.Open ();

				using (var command = Connection.CreateCommand ()) {
					command.CommandText = "SELECT [Id], [Name], [Color] " +
										  "FROM [Category]";

					var r = command.ExecuteReader ();

					while (r.Read ()) {
						tl.Add (CategoryReader(r));
					}
				}

				Connection.Close ();
			}

			return tl;
		}

        public Category GetCategory (int id) 
		{
			var c = new Category ();

			lock (locker) {
				Connection = new SqliteConnection ("Data Source=" + Path);

				Connection.Open ();

				using (var command = Connection.CreateCommand ()) {
					command.CommandText = "SELECT [Id], [Name], [Color] " +
										  "FROM [Category]" +
										  "WHERE [Id] = ?";

					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });

					var r = command.ExecuteReader ();

					
					while (r.Read ()) {
						c = CategoryReader (r);
						break;
					}
				}

				Connection.Close ();
			}

			return c;
		}

		public int SaveCategory (Category item) 
		{
			int r;

			lock (locker) {

                if (item.Id != 0) {
					Connection = new SqliteConnection ("Data Source=" + Path);

					Connection.Open ();

					using (var command = Connection.CreateCommand ()) {
						command.CommandText = "UPDATE [Category] " +
											  "SET [Name] = ?, [Color] = ?" +
                                              "WHERE [Id] = ?;";

						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = (int)item.Color });
                        command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Id });

						r = command.ExecuteNonQuery ();

					}

					Connection.Close ();

					return r;
				} else {
					Connection = new SqliteConnection ("Data Source=" + Path);

					Connection.Open ();

					using (var command = Connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [Category] ([Id], [Name], [Color]) " +
											  "VALUES (NULL, ?, ?)";

						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = (int)item.Color });

						r = command.ExecuteNonQuery ();

					}

					Connection.Close ();

					return r;
				}

			}
		}

		public int DeleteCategory(int id) 
		{
			GetTasks(id).ToList().ForEach(t => {
				DeleteTask(id);
			});

			lock (locker) {
				int r;

				Connection = new SqliteConnection ("Data Source=" + Path);

				Connection.Open ();

				using (var command = Connection.CreateCommand ()) {
					command.CommandText = "DELETE FROM [Category]" +
										  "WHERE [Id] = ?;";

					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});

					r = command.ExecuteNonQuery ();

				}

				Connection.Close ();

				return r;
			}
		}

		Task TaskReader (SqliteDataReader r) {
			var t = new Task ();

            t.Id    	 = Convert.ToInt32 (r ["Id"]);
			t.Name  	 = r ["Name"].ToString ();
			t.Notes 	 = r ["Notes"].ToString ();
			t.Done  	 = Convert.ToInt32 (r ["Done"]) == 1 ? true : false;
			t.CategoryId = Convert.ToInt32 (r ["CategoryId"]);

			if (r ["Date"].ToString().Length == 0) {
				t.DueDate = false;
			} else {
				t.DueDate = true;
				t.Date	  = Convert.ToInt32(r ["Date"]).FromUnix();
			}

			return t;
		}

		public IEnumerable<Task> GetTasks (int categoryId)
		{
			var tl = new List<Task> ();

			lock (locker) {
				Connection = new SqliteConnection ("Data Source=" + Path);

				Connection.Open ();

				using (var command = Connection.CreateCommand ()) {
                    command.CommandText = "SELECT [Id], [Name], [Notes], [Done], [Date], [CategoryId] " +
										  "FROM [Task] " +
										  "WHERE [CategoryId] = ?" +
										  "ORDER BY Done,Date ASC";

					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = categoryId });

					var r = command.ExecuteReader ();

					while (r.Read ()) {
						tl.Add (TaskReader(r));
					}
				}

				Connection.Close ();
			}

			return tl;
		}

		public Task GetTask (int id) 
		{
			var t = new Task ();

			lock (locker) {
				Connection = new SqliteConnection ("Data Source=" + Path);

				Connection.Open ();

				using (var command = Connection.CreateCommand ()) {
                    command.CommandText = "SELECT [Id], [Name], [Notes], [Done], [Date], [CategoryId] " +
                                          "FROM [Task] WHERE" +
										  "[Id] = ?";

					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });

					var r = command.ExecuteReader ();

					
					while (r.Read ()) {
						t = TaskReader (r);
						break;
					}
				}

				Connection.Close ();
			}

			return t;
		}

		public int SaveTask (Task item) 
		{
			int r;

			lock (locker) {
                if (item.Id != 0) {
					Connection = new SqliteConnection ("Data Source=" + Path);

					Connection.Open ();

					using (var command = Connection.CreateCommand ()) {


						command.CommandText = "UPDATE [Task] ";

						if (item.DueDate) {
							command.CommandText += "SET [Name] = ?, [Notes] = ?, [Done] = ?, [Date] = ?, [CategoryId] = ? ";
						} else {
							command.CommandText += "SET [Name] = ?, [Notes] = ?, [Done] = ?, [Date] = NULL, [CategoryId] = ? ";
						}
                        
						command.CommandText += "WHERE [Id] = ?;";

						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Notes });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Done });

						if (item.DueDate) {
							command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.Date.ToUnix() });
						}

						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.CategoryId });
                        command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Id });

						r = command.ExecuteNonQuery ();

					}

					Connection.Close ();

					return r;
				} else {
					Connection = new SqliteConnection ("Data Source=" + Path);
					Connection.Open ();

					using (var command = Connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [Task] ([Id], [Name], [Notes], [Done], [Date], [CategoryId])";

						if (item.DueDate) {
							command.CommandText += "VALUES (NULL, ?, ?, ?, ?, ?)";
						} else {
							command.CommandText += "VALUES (NULL, ?, ?, ?, NULL, ?)";
						}

						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Notes });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Done });


						if (item.DueDate) {
							command.Parameters.Add(new SqliteParameter(DbType.Int32) { Value = item.Date.ToUnix() });
						}

						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.CategoryId });

						r = command.ExecuteNonQuery ();

					}

					Connection.Close ();

					return r;
				}

			}
		}

		public int DeleteTask(int id) 
		{
			lock (locker) {
				int r;

				Connection = new SqliteConnection ("Data Source=" + Path);

				Connection.Open ();

				using (var command = Connection.CreateCommand ()) {
					command.CommandText = "DELETE FROM [Task] " +
										  "WHERE [Id] = ?;";

					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});

					r = command.ExecuteNonQuery ();

				}

				Connection.Close ();

				return r;
			}
		}
	}
}