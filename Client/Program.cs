using Client.SCSService;
using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			TestSCS();
		}

		static void TestSCS()
		{
			string connectionPort;
			while (true)
			{
				
				Console.WriteLine("Connect on port 5000 (primary) or 5001 (primary)?");
				if(Console.ReadLine().Trim() == "5000")
					connectionPort = "5000";
				else
					connectionPort = "5001";

				Connection.CreateSCServiceProxy(connectionPort);
				
				Requests requests = new Requests();
				requests.AddSmartCard();
				requests.ChangeUserPin();

				Connection.CloseSCServiceProxy();
				
				Console.WriteLine("Press 0 key to exit any key to continue...");
				if (Console.ReadKey().KeyChar.ToString() == "0")
					break;
			}
		}
	}
}
