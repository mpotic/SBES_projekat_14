using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Services
{
	[ServiceContract]
	internal interface ReplicatorServiceContract
	{
		[OperationContract]
		bool ReplicateUserRegistration(User user);

		[OperationContract]
		bool ReplicateUserUpdatePin(User user);
		[OperationContract]
		bool ReplicateValidatePayout(string stringAmount, string subjectName);

		[OperationContract]
		bool ReplicateValidateDeposit(string stringAmount, string subjectName);

	}
}
