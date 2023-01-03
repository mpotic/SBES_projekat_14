using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Services;
using SmartCardsService.Features;
using SmartCardsService.Models;

namespace SmartCardsService.Services
{
	internal class SCService : SCServiceContract
	{
		private UserManager UserManager { get; set; } = new UserManager();

		public bool CreateSmartCard(User user)
		{
			return UserManager.RegisterNewUser(user);
		}
	}
}
