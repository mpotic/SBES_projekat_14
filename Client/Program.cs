using Client.ATMService;
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

				string ATMconnectionPort = "5002";
				Connection.CreateSCServiceProxy(connectionPort);
				ATMConnection.CreateATMServiceProxy(ATMconnectionPort);
				
				Requests requests = new Requests();
				requests.AddSmartCard();
				requests.ChangeUserPin();

				Connection.CloseSCServiceProxy();
				ATMConnection.CloseATMServiceProxy();
				
				Console.WriteLine("Press 0 key to exit or any other key to continue...");
				if (Console.ReadKey().KeyChar.ToString() == "0")
					break;
			}
		}
	}
}
