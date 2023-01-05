using SmartCardsService.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Features
{
	internal enum ServiceTypeEnum { Primary = 0, Secondary };
	internal class Replication
	{
		private static ServiceTypeEnum serviceType;

		internal static ServiceTypeEnum ServiceType { get => serviceType; set => serviceType = value; }

		internal bool SetServiceType()
		{
			int type = -1;
			
			Console.Write("Enter 0 to start the service as primary or press 1 so start the service as secondary: ");
			
			while(!int.TryParse(Console.ReadLine(), out type) || (type != 0 && type != 1))
			{
				Console.WriteLine("Enter only 0 or 1: ");
			}

			ServiceType = (ServiceTypeEnum)type;

			if (ServiceType == ServiceTypeEnum.Secondary)
				DatabaseCRUD.DbConnection.ConnectionString = 
					$@"Server=DESKTOP-SUTD4IG\SQLEXPRESS;Initial Catalog=SBES_Replicator;Integrated Security=true;";

			return true;
		}
	}
}
