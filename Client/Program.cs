using Client.SCSService;
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
			Connection.CreateSCServiceProxy();
			Requests requests = new Requests();

			requests.AddSmartCard();

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}
	}
}
