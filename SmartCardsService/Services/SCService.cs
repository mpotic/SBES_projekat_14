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
			if (UserManager.RegisterNewUser(new User(user)))
				return WCFManager.ReplicatorProxy.ReplicateUserRegistration(user);

			return false;
		}

		public bool ChangePin(User user)
		{

			bool status = UserManager.ChangeUserPin(new User(user));
			if (status)
				Audit.PinChangeSuccess("SmartCardService/" + Replication.ServiceType.ToString(), user.SubjectName);
			else
				Audit.PinChangeFailure("SmartCardService/" + Replication.ServiceType.ToString(), user.SubjectName);

			if (status)
				status = WCFManager.ReplicatorProxy.ReplicateUserUpdatePin(user);

			return status;
		}
	}
}
