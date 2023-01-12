using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomLogger;
using SCSCommon.Services;
using SmartCardsService.Connections;
using SmartCardsService.Features;
using SmartCardsService.Models;

namespace SmartCardsService.Services
{
	internal class SCService : SCServiceContract
	{
		private UserManager UserManager { get; set; } = new UserManager();

		public bool CreateSmartCard(User user)
		{
			if (!UserManager.RegisterNewUser(new User(user)))
			{
				Audit.CardCreationFailure(Replication.ServiceType.ToString(), user.SubjectName);
				return false;
			}

			Audit.CardCreationSuccess(Replication.ServiceType.ToString(), user.SubjectName);

			WCFManager.ReplicatorProxy.ReplicateUserRegistration(user);

			//Make exchange certificates
			Console.WriteLine("\n----------------------- M A K E - C E R T I F I C A T E S -----------------------");
			Certificates.GenerateCertificate(user.SubjectName, user.OrganizationalUnit, user.SubjectName + "_exchange", false);
			Console.WriteLine("Repeat the password:");
			string password = Console.ReadLine().Trim();
			Certificates.GeneratePFX(user.SubjectName + "_exchange", password);

			//Make sign certificates
			Certificates.GenerateCertificate(user.SubjectName, user.OrganizationalUnit, user.SubjectName + "_signature", false);
			Console.WriteLine("Repeat the password:");
			password = Console.ReadLine().Trim();
			Certificates.GeneratePFX(user.SubjectName + "_signature", password);
			Console.WriteLine("---------------------------------------------------------------------------------\n");

			return true;
		}

		public bool ChangePin(User user)
		{
			bool status = UserManager.ChangeUserPin(new User(user));
			if (status)
			{
				Audit.PinChangeSuccess("SmartCardService/" + Replication.ServiceType.ToString(), user.SubjectName);
				status = WCFManager.ReplicatorProxy.ReplicateUserUpdatePin(user);
			}
			else
				Audit.PinChangeFailure("SmartCardService/" + Replication.ServiceType.ToString(), user.SubjectName);

			return status;
		}
	}
}
