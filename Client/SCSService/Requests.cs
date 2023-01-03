using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.SCSService
{
	internal class Requests
	{
		internal bool AddSmartCard()
		{
			Console.WriteLine("SubjectName:\t");
			string subjectName = Console.ReadLine();
			if (subjectName.Trim().Length > 30 || subjectName.Trim().Length < 1)
			{
				Console.WriteLine("Name has invalid length!");
				return false;
			}
			subjectName = subjectName.Trim();

			Console.WriteLine("OrganizationalUnit (1 = Manager / 2 = SmartCardUser):\t");
			string organizationalUnit = Console.ReadLine();
			if (organizationalUnit.Trim() == "1")
			{
				organizationalUnit = "Manager";
			}
			else if (organizationalUnit.Trim() == "2")
			{
				organizationalUnit = "SmartCardUser";
			}
			else
			{
				Console.WriteLine("Wrong organizational unit!");
				return false;
			}

			Console.WriteLine("Pin (4-digit):\t");
			string pin = Console.ReadLine();
			if (pin.Trim().Length != 4)
			{
				Console.WriteLine("Wrong pin number!");
				return false;
			}

			return Connection.ScsProxy.CreateSmartCard(new User(subjectName, pin, organizationalUnit, ""));
		}

		internal bool ChangeUserPin()
		{
			Console.WriteLine("SubjectName:\t");
			string subjectName = Console.ReadLine();
			if (subjectName.Trim().Length > 30 || subjectName.Trim().Length < 1)
			{
				Console.WriteLine("Name has invalid length!");
				return false;
			}
			subjectName = subjectName.Trim();

			Console.WriteLine("Current Pin (4-digit):\t");
			string pin = Console.ReadLine().Trim();
			if (pin.Length != 4)
			{
				Console.WriteLine("Wrong pin number!");
				return false;
			}

			Console.WriteLine("New Pin (4-digit):\t");
			string newPin = Console.ReadLine().Trim();
			if (pin.Length != 4)
			{
				Console.WriteLine("Wrong pin number!");
				return false;
			}

			return Connection.ScsProxy.ChangePin(new User(subjectName, pin, "", newPin));
		}
	}
}
