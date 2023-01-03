using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Connections
{
	internal class DatabaseConnection
	{
		internal SqlConnection connection = null;
		internal string ConnectionString { get; set; } = 
			$@"Server=DESKTOP-SUTD4IG\SQLEXPRESS;Initial Catalog=SBES;Integrated Security=true;";

		internal void OpenConnection()
		{
			try
			{
				if(connection == null)
					connection = new SqlConnection(ConnectionString);

				if(connection.State != ConnectionState.Open)
					connection.Open();
			}
			catch(Exception e)
			{
				Console.WriteLine("ERROR while trying to connect to the database: " + e.Message);
				CloseConnection();
			}
		}
		
		public void CloseConnection()
		{
			if (connection != null && connection.State != ConnectionState.Closed)
			{ 
				connection.Close();
				connection.Dispose();
			}
		}
	}
}
