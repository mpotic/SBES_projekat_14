using SmartCardsService.Features;
using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Connections
{
	internal static class DatabaseCRUD
	{
		private static string tableName = "SmartCards";
		private static DatabaseConnection dbConnection = new DatabaseConnection();
		internal static string TableName { get => tableName; set => tableName = value; }
		internal static DatabaseConnection DbConnection { get => dbConnection; set => dbConnection = value; }

		internal static bool InsertNewUser(User user)
		{
			try
			{
				dbConnection.OpenConnection();

				SqlDataAdapter adapter = new SqlDataAdapter();
				SqlCommand command;

				string query = $"Insert into {TableName} (SubjectName, OrganizationalUnit, Pin, Balance)" + 
							$"values('{user.SubjectName}', '{user.OrganizationalUnit}', '{user.Pin}', 0)";

				command = new SqlCommand(query, dbConnection.connection);

				adapter.InsertCommand = command;
				adapter.InsertCommand.ExecuteNonQuery();

				dbConnection.CloseConnection();
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to insert a new user into the database on " +
					$"{Replication.ServiceType.ToString()} server: " + e.Message);

				return false;
			}

			return true;
		}

		internal static bool UpdateUserPin(User user)
		{
			try
			{
				dbConnection.OpenConnection();

				SqlDataAdapter adapter = new SqlDataAdapter();
				SqlCommand command;

				string query = $"Update {tableName} set Pin='{user.NewPin}' where SubjectName='{user.SubjectName}'";

				command = new SqlCommand(query, dbConnection.connection);

				adapter.UpdateCommand = command;
				adapter.UpdateCommand.ExecuteNonQuery();

				dbConnection.CloseConnection();
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to update the users PIN on " +
					$"{Replication.ServiceType.ToString()} server: " + e.Message);

				return false;
			}

			return true;
		}

		internal static bool ExistsUserWithPin(User user)
		{
			try
			{
				dbConnection.OpenConnection();

				SqlCommand command;
				string query = $@"Select 1 from {TableName} where SubjectName='{user.SubjectName}' and Pin='{user.Pin}'";
				command = new SqlCommand(query, dbConnection.connection);

				SqlDataReader reader = command.ExecuteReader();

				bool result = false;
				if (reader.HasRows)
					result = true;

				dbConnection.CloseConnection();

				return result;
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to check if the users with specific PIN exists on " +
					$"{Replication.ServiceType.ToString()} server: " + e.Message);

				return false;
			}
		}

		internal static User GetUser(string subjectName)
		{
			try
			{
				dbConnection.OpenConnection();

				SqlCommand command;
				User user = null;
				string query = $@"Select * from {TableName} where SubjectName='{subjectName}'";
				command = new SqlCommand(query, dbConnection.connection);

				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					user = new User()
					{
						SubjectName = reader.GetString(0),
						OrganizationalUnit = reader.GetString(1),
						Pin = reader.GetString(2),
						Amount = double.Parse(reader.GetDecimal(3).ToString())
					};
				}

				reader.Close();
				dbConnection.CloseConnection();

				return user;
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to get the user named {subjectName} on " +
					$"{Replication.ServiceType.ToString()} server: " + e.Message);

				return null;
			}
		}

		internal static bool WriteBalance(string subjectName, double newBalance)
		{
			try
			{
				dbConnection.OpenConnection();

				string query = $"Update {tableName} set Balance='{newBalance}' where SubjectName='{subjectName}'";
				SqlCommand command = new SqlCommand(query, dbConnection.connection);

				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.UpdateCommand = command;
				
				if (adapter.UpdateCommand.ExecuteNonQuery() <= 0)
				{
					dbConnection.CloseConnection();
					return false;
				}

				dbConnection.CloseConnection();

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to write new balance on {subjectName} smart card account on " +
					$"{Replication.ServiceType.ToString()} server: " + e.Message);

				return false;
			}
		}

		internal static List<User> GetAllUsers()
		{
			dbConnection.OpenConnection();

			List<User> users = new List<User>();
			string query = $"Select SubjectName, OrganizationalUnit, Pin from {TableName}";
			SqlCommand command = new SqlCommand(query, dbConnection.connection);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				users.Add(new User(reader.GetValue(0).ToString().Trim(),
					reader.GetValue(2).ToString().Trim(),
					reader.GetValue(1).ToString().Trim(), ""));
			}

			reader.Close();
			dbConnection.CloseConnection();

			return users;
		}
	}
}
