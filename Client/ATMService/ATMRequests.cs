using CertificateManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
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
			string message = subjectName + "+" + pin;

			//string clientCertCN = "test";
			string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);
			byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);


			return ATMConnection.ATMProxy.ValidateSmartCardPin(message, signature);

        }

		public bool Payment()
        {
			Console.WriteLine("Enter amount you want to cash in: ");
			string amount = Console.ReadLine();
			int number;
			bool isNumber = int.TryParse(amount, out number);
			if (!isNumber)
			{
				Console.WriteLine("Enter valid number!");
				return false;
			}
			//string clientCertCN = "test";
			string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			string message = amount + "+" + clientCertCN;
			X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);
			byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);
			return ATMConnection.ATMProxy.CheckPayment(message, signature);
		}

		public bool Payout()
        {
			Console.WriteLine("Enter amount you want to cash out: ");
			string amount = Console.ReadLine();
			int number;
			bool isNumber = int.TryParse(amount, out number);
            if (!isNumber)
            {
                Console.WriteLine("Enter valid number!");
				return false;
            }
			//string clientCertCN = "test";
			string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			string message = amount + "+" + clientCertCN;
			X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);
			byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);
			return ATMConnection.ATMProxy.CheckPayout(message, signature);
        }
    }
}
