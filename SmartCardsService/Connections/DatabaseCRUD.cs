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

				string query = $@"Insert into {TableName} (SubjectName, OrganizationalUnit, Pin) 
							values('{user.SubjectName}', '{user.OrganizationalUnit}', '{user.Pin}')";

				command = new SqlCommand(query, dbConnection.connection);

				adapter.InsertCommand = command;
				adapter.InsertCommand.ExecuteNonQuery();

				dbConnection.CloseConnection();
			}
			catch (Exception e)
			{
				Console.WriteLine($@"ERROR while trying to insert a new user into the database on 
							{Replication.ServiceType.ToString()} server: " + e.Message);

				return false;
			}

			return true;
		}

		internal static void PrintAllUsers()
		{
			dbConnection.OpenConnection();

			string query = $"Select SubjectName, OrganizationalUnit, Pin from {TableName}";
			SqlCommand command = new SqlCommand(query, dbConnection.connection);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				Console.Write("SubjectName: " + reader.GetValue(0).ToString().Trim() + ", ");
				Console.Write("OrganizationalUnit: " + reader.GetValue(1).ToString().Trim() + ", ");
				Console.WriteLine("Pin: " + reader.GetValue(2).ToString().Trim());
			}

			dbConnection.CloseConnection();
		}
	}
}
