using SmartCardsService.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Features
{
	internal enum ServiceTypeEnum { Primary = 1, Replicator };
	internal class Replication
	{
		private static ServiceTypeEnum serviceType;

		internal static ServiceTypeEnum ServiceType { get => serviceType; set => serviceType = value; }

		internal bool SetServiceType()
		{
			int type = 0;
			
			Console.Write("Enter 1 to start the service as a primary service or press 2 so start the service as a replicator: ");
			
			while(!int.TryParse(Console.ReadLine(), out type) || (type != 1 && type != 2))
			{
				Console.WriteLine("Enter only 1 or 2: ");
			}

			ServiceType = (ServiceTypeEnum)type;

			if (ServiceType == ServiceTypeEnum.Primary)
				DatabaseCRUD.TableName = "SmartCards";
			else
				DatabaseCRUD.TableName = "SmartCardsReplicator";

			return true;
		}
	}
}
