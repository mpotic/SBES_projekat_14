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
	internal class ValidationService : ValidationServiceContract
	{
		private UserManager UserManager { get; set; } = new UserManager();
		private Validation Validation { get; set; } = new Validation();

 		public bool ValidateSmartCardPin(User user)
		{
			
			return UserManager.CheckPinValidity(user);
		}

		public List<User> GetAllUsers()
		{
			return DatabaseCRUD.GetAllUsers();
		}

		public bool ValidateDeposit(string amount, string subjectName)
        {
			return Validation.ValidateAndProcessDeposit(subjectName, amount);
        }

		public bool ValidatePayout(string amount, string subjectName)
        {
			return Validation.ValidateAndProcessPayout(subjectName, amount);
        }
	}
}
