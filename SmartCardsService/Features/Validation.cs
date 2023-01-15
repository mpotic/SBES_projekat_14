using SmartCardsService.Connections;
using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Features
{
	internal class Validation
	{
		internal bool ValidateAndProcessDeposit(string subjectName, string stringAmount)
		{			
			bool status = false;
			double amount = 0;
			if (!double.TryParse(stringAmount, out amount))
				return false;
			if (amount < 0)
				return false;

			User user = DatabaseCRUD.GetUser(subjectName);
			if (user == null)
				return false;

			status = DatabaseCRUD.WriteBalance(subjectName, amount + user.Amount);

			WCFManager.ReplicatorProxy.ReplicateValidateDeposit(stringAmount, subjectName);

			return status;
		}

		internal bool ValidateAndProcessPayout(string subjectName, string stringAmount)
		{
			bool result = false;
			double amount = 0;
			if (!double.TryParse(stringAmount, out amount))
				return false;

			if (amount < 0)
				return false;

			User user = DatabaseCRUD.GetUser(subjectName);
			if (user == null)
				return false;

			if(user.Amount >= amount)
			{
				result = DatabaseCRUD.WriteBalance(subjectName, user.Amount - amount);
				WCFManager.ReplicatorProxy.ReplicateValidatePayout(stringAmount, subjectName);
			}

			return result;
		}
	}
}
