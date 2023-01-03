using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCardsService.Connections;
using SmartCardsService.Models;
using SmartCardsService.Features;

namespace SmartCardsService
{
	class Program
	{
		static void Main(string[] args)
		{
			WCFManager wcfManager;
			Replication replication = new Replication();

			replication.SetServiceType();

			wcfManager = new WCFManager(Replication.ServiceType == ServiceTypeEnum.Primary ? 5000 : 5001);
			wcfManager.OpenSCServiceHost();

			ShutDown(wcfManager);
		}

		static void ShutDown(WCFManager wcfManager)
		{
			Console.WriteLine("Press any key to exit shutdown the server...");
			Console.ReadKey();
			wcfManager.CloseSCServiceHost();
		}
	}
}
