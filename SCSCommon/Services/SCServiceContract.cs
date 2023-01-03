using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Services
{
	[ServiceContract]
	public interface SCServiceContract
	{
		[OperationContract]
		bool CreateSmartCard(User user);
		[OperationContract]
		bool ChangePin(User user);
	}
}
