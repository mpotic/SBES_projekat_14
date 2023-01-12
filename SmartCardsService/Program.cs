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
			Replication replication = new Replication();
			replication.SetServiceType();
			WCFManager.ServicePort = Replication.ServiceType == ServiceTypeEnum.Primary ? 5000 : 5001;
			
			WCFManager.OpenSCServiceHost();
			WCFManager.OpenReplicatorServiceHost();
			WCFManager.OpenValidationServiceHost();

			ShutDown();
		}

		static void ShutDown()
		{
			Console.WriteLine("Press any key to exit shutdown the server...");
			Console.ReadKey();

			WCFManager.CloseSCServiceHost();
			WCFManager.CloseReplicatorServiceHost();
			WCFManager.CloseValidationServiceHost();

			WCFManager.CloseReplicatorProxy();
		}
	}
}
