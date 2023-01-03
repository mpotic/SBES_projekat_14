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
	}
}
