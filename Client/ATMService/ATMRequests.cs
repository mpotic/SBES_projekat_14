using CertificateManager;
using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
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

			string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);
			byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);

			return ATMConnection.ATMProxy.ValidateSmartCardPin(message, signature);

		}

		internal bool Payment()
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
			string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			string message = amount + "+" + clientCertCN;
			X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);
			byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);
			return ATMConnection.ATMProxy.CheckPayment(message, signature);
		}

		internal bool Payout()
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
			string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			string message = amount + "+" + clientCertCN;
			X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);
			byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);
			return ATMConnection.ATMProxy.CheckPayout(message, signature);
		}

		internal bool ListAllUsers()
		{

			Console.WriteLine("Trying to get all users...");
			Tuple<byte[], string> tuple;

			string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);
			byte[] signatureClient = DigitalSignature.Create(clientCertCN, HashAlgorithm.SHA1, certificateSign);

			tuple = ATMConnection.ATMProxy.GetAllUsers(clientCertCN, signatureClient);
			if (tuple == null)
			{
				return false;
			}
			byte[] signature = tuple.Item1;
			string users = tuple.Item2;

			string serviceCertCN = "atmservice";
			X509Certificate2 serviceCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, serviceCertCN);
			if (DigitalSignature.Verify(users, HashAlgorithm.SHA1, signature, serviceCertificate))
			{
				Console.WriteLine("Sign is valid");
				Console.WriteLine("List of all users: ");
				string[] listUsers = users.Split(';');

				foreach (var user in listUsers)
				{
					string[] singleUser = user.Split('+');
					Console.WriteLine("Subject name: " + singleUser[0]);
					Console.WriteLine("Organizational unit: " + singleUser[1]);
				}
				return true;
			}
			else
			{
				Console.WriteLine("Sign is invalid");
				return false;
			}
		}

	}
}
