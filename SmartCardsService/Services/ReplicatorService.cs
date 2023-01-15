using CustomLogger;
using SmartCardsService.Connections;
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
			bool status = UserManager.RegisterNewUser(user);
			if (status)
				Audit.ReplicationSuccess("ReplicatorService/" + Replication.ServiceType.ToString(), user.SubjectName);
			else
				Audit.ReplicationSuccess("ReplicatorService/" + Replication.ServiceType.ToString(), user.SubjectName);

			return status;
		}

		public bool ReplicateUserUpdatePin(User user)
		{
			bool status =  UserManager.ChangeUserPin(user);
			if (status)
				Audit.PinReplicationSuccess("ReplicatorService/" + Replication.ServiceType.ToString(), user.SubjectName);
			else
				Audit.PinReplicationSuccess("ReplicatorService/" + Replication.ServiceType.ToString(), user.SubjectName);

			return status;
		}

		public bool ReplicateValidateDeposit(string stringAmount, string subjectName)
		{
			double amount = 0;
			if (!double.TryParse(stringAmount, out amount))
				return false;

			User user = DatabaseCRUD.GetUser(subjectName);
			if (user == null)
				return false;

			bool result = DatabaseCRUD.WriteBalance(subjectName, amount + user.Amount);

			return result;
		}

		public bool ReplicateValidatePayout(string stringAmount, string subjectName)
		{
			double amount = 0;
			if (!double.TryParse(stringAmount, out amount))
				return false;

			User user = DatabaseCRUD.GetUser(subjectName);
			if (user == null)
				return false;

			if (user.Amount >= amount)
				return DatabaseCRUD.WriteBalance(subjectName, user.Amount - amount);

			return false;
		}
	}
}
