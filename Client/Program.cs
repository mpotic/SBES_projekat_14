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
			bool authenticated = false;

			Console.WriteLine("Connect on port 5000 (primary) or 5001 (secondary)?");
			if (Console.ReadLine().Trim() == "5000")
				connectionPort = "5000";
			else
				connectionPort = "5001";

			Connection.CreateSCServiceProxy(connectionPort);

			while (true)
			{



				Requests requests = new Requests();
				ATMRequests atmRequests = new ATMRequests();



				Console.WriteLine("---MENU---");
				int number = 0;

				if (!authenticated)
				{
					Console.WriteLine("1. Add new smart card.");
					Console.WriteLine("2. Change pin code.");
					Console.WriteLine("3. Authenticate.");
					Console.WriteLine("Enter a number (1 - 3): ");
					number = int.Parse(Console.ReadLine());
					switch (number)
					{

						case 1:
							if (requests.AddSmartCard())
							{
								Console.WriteLine("Successfully added new card. ");
							}
							else
							{
								Console.WriteLine("Adding new card failed. ");
							}
							break;
						case 2:
							if (requests.ChangeUserPin())
							{
								Console.WriteLine("Successfully changed users pin.");
							}
							else
							{
								Console.WriteLine("Changing pin failed. ");
							}
							break;
						case 3:
							string ATMconnectionPort = "5002";
							Console.WriteLine("Connecting to ATM.");
							ATMConnection.CreateATMServiceProxy(ATMconnectionPort);
							if (atmRequests.Authentication())
							{
								Console.WriteLine("Authentication succeeded.");
								authenticated = true;
							}
							else
							{
								Console.WriteLine("Authenication failed. ");

							}
							break;
						default:
							Console.WriteLine("Enter a valid number.");
							break;
					}

				}

				if (authenticated)
				{
					Console.WriteLine("1. Cash in.");
					Console.WriteLine("2. Cash out.");
					Console.WriteLine("3. List all users.");
					Console.WriteLine("4. Log out.");
					Console.WriteLine("Enter a number (1 - 4): ");
					int number1 = 0;
					number1 = int.Parse(Console.ReadLine());
					switch (number1)
					{
						case 1:
							if (atmRequests.Payment())
							{
								Console.WriteLine("Successfully cashed in.");
							}
							else
							{
								Console.WriteLine("Cash in failed. ");
							}
							break;
						case 2:
							if (atmRequests.Payout())
							{
								Console.WriteLine("Successfully cashed out.");
							}
							else
							{
								Console.WriteLine("Cash out failed.");
							}
							break;
						case 3:
							if (!atmRequests.ListAllUsers())
							{
								Console.WriteLine("Listing all users failed.");
							}
							break;
						case 4:
							authenticated = false;
							Console.WriteLine("Successfully logged out.");
							break;
						default:
							Console.WriteLine("Enter a valid number. ");
							break;
					}


				}



				Console.WriteLine("Press 0 key to exit or any other key to continue...");
				if (Console.ReadKey().KeyChar.ToString() == "0")
				{
					Connection.CloseSCServiceProxy();
					ATMConnection.CloseATMServiceProxy();
					break;

				}
			}
		}
	}
}
