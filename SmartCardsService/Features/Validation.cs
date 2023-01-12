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
			double amount = 0;
			if (!double.TryParse(stringAmount, out amount))
				return false;

			User user = DatabaseCRUD.GetUser(subjectName);
			if (user == null)
				return false;

			return DatabaseCRUD.WriteBalance(subjectName, amount + user.Amount);
		}

		internal bool ValidateAndProcessPayout(string subjectName, string stringAmount)
		{
			double amount = 0;
			if (!double.TryParse(stringAmount, out amount))
				return false;

			User user = DatabaseCRUD.GetUser(subjectName);
			if (user == null)
				return false;

			if(user.Amount >= amount)
				return DatabaseCRUD.WriteBalance(subjectName, user.Amount - amount);

			return false;
		}
	}
}
