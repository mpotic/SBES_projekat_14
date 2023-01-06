using SmartCardsService.Connections;
using SmartCardsService.Features;
using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Services
{
	public class ValidationService : ValidationServiceContract
	{
		private UserManager UserManager { get; set; } = new UserManager();
		public bool ValidateSmartCardPin(User user)
		{
			return UserManager.CheckPinValidity(user);
		}
		public List<User> GetAllUsers()
		{
			return DatabaseCRUD.GetAllUsers();
		}
	}
}
