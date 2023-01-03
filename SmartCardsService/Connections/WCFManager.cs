using SCSCommon.Services;
using SmartCardsService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Connections
{
	internal class WCFManager
	{
		int servicePort;
		ServiceHost scServiceHost;
		ServiceHost validationServiceHost;
		ServiceHost replicationServiceHost;

		public WCFManager(int port)
		{
			servicePort = port;
		}

		public int ServicePort { get => servicePort; set => servicePort = value; }
		public ServiceHost SCServiceHost { get => scServiceHost; set => scServiceHost = value; }
		public ServiceHost ValidationServiceHost { get => validationServiceHost; set => validationServiceHost = value; }
		public ServiceHost ReplicationServiceHost { get => replicationServiceHost; set => replicationServiceHost = value; }

		internal bool OpenSCServiceHost()
		{
			try
			{
				SCServiceHost = new ServiceHost(typeof(SCService));
				SCServiceHost.AddServiceEndpoint(typeof(SCServiceContract),
					new NetTcpBinding(), $"net.tcp://localhost:{ServicePort}/SCService");
				SCServiceHost.Open();
				Console.WriteLine($"SmartCardService host ready as \"net.tcp://localhost:{ServicePort}/SCService\"!");
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR while trying to open host at SmartCardsService endpoint: " + e.Message);
				return false;
			}
			return true;
		}

		internal bool CloseSCServiceHost()
		{
			try
			{ 
				SCServiceHost.Close();
				Console.WriteLine("SmartCardService host closed!");
			}
			catch(Exception e)
			{
				Console.WriteLine("ERROR while trying to close SmartCardsService host: " + e.Message);
				return false;
			}
			
			return true;
		}

		internal bool OpenValidationServiceEndpoint()
		{
			return true;
		}
		internal bool CloseValidationServiceEndpoint()
		{
			return true;
		}

	}
}
