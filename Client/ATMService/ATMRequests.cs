using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ATMService
{
    internal class ATMRequests
    {
        internal bool Authentication()
        {
            Console.WriteLine("Authentication is in progress, please enter your name and pin number!");
			Console.WriteLine("SubjectName:\t");
			string subjectName = Console.ReadLine();
			if (subjectName.Trim().Length > 30 || subjectName.Trim().Length < 1)
			{
				Console.WriteLine("Name has invalid length!");
				return false;
			}
			subjectName = subjectName.Trim();

			Console.WriteLine("Pin (4-digit):\t");
			string pin = Console.ReadLine();
			if (pin.Trim().Length != 4)
			{
				Console.WriteLine("Wrong pin number!");
				return false;
			}

			return ATMConnection.ATMProxy.ValidateSmartCardPin(subjectName, pin);

        }
    }
}
