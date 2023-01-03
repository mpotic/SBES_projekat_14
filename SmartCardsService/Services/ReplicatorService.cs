using CustomLogger;
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
	}
}
