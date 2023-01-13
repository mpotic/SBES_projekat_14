using ATM.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
			WCFManager.ServicePort = 5002;
			WCFManager.OpenATMHost();
			string connectionPort = "5000";
			WCFManager.CreateValidationServiceProxy(connectionPort);

			ShutDown();

		}

		static void ShutDown()
		{
			Console.WriteLine("Press any key to exit shutdown the server...");
			Console.ReadKey();

			WCFManager.CloseSCServiceHost();
			WCFManager.CloseATMServiceProxy();
		}
	}
}
