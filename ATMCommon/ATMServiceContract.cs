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
		[OperationContract]
		bool ValidateSmartCardPin(string message, byte[] clientSignature);
		[OperationContract]
		Tuple<byte[], string> GetAllUsers(string message, byte[] clientSignature);
		[OperationContract]
		bool CheckPayment(string message, byte[] clientSignature);
		[OperationContract]
		bool CheckPayout(string message, byte[] clientSignature);
	}
}
