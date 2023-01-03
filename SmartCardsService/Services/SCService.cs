using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			if (WCFManager.ReplicatorProxy == null)
				WCFManager.CreateReplicatorProxy();

			WCFManager.ReplicatorProxy.ReplicateUserRegistration(user);

			return UserManager.RegisterNewUser(user);
		}
	}
}
