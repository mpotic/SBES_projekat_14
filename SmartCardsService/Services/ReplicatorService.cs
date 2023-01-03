using SmartCardsService.Features;
using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Services
{
	class ReplicatorService : ReplicatorServiceContract
	{
		private UserManager UserManager { get; set; } = new UserManager();
		public bool ReplicateUserRegistration(User user)
		{
			return UserManager.RegisterNewUser(user);
		}
	}
}
