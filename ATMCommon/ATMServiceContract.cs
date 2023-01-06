using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ATMCommon
{
	[ServiceContract]
	public interface ATMServiceContract
	{
		//[OperationContract]
		//Metode ATMa
		[OperationContract]
		bool ValidateSmartCardPin(string subjectName, string pin);
		[OperationContract]
		List<User> GetAllUsers();
	}
}
