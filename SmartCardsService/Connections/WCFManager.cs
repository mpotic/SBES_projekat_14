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
	internal static class WCFManager
	{
		private static int servicePort;
		private static ServiceHost scServiceHost;
		private static ServiceHost validationServiceHost;
		private static ServiceHost replicatorServiceHost;
		private static ChannelFactory<ReplicatorServiceContract> factory;
		private static ReplicatorServiceContract replicatorProxy;

		internal static int ServicePort { get => servicePort; set => servicePort = value; }
		internal static ServiceHost SCServiceHost { get => scServiceHost; set => scServiceHost = value; }
		internal static ServiceHost ValidationServiceHost { get => validationServiceHost; set => validationServiceHost = value; }
		internal static ServiceHost ReplicatorServiceHost { get => replicatorServiceHost; set => replicatorServiceHost = value; }
		internal static ReplicatorServiceContract ReplicatorProxy { get => replicatorProxy; set => replicatorProxy = value; }

		internal static bool OpenSCServiceHost()
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
				Console.WriteLine($"ERROR while trying to open host at \"net.tcp://localhost:{ServicePort}/SCService\" endpoint: {e.Message}");
				return false;
			}
			return true;
		}

		internal static bool CloseSCServiceHost()
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

		internal static bool OpenReplicatorServiceHost()
		{
			try
			{
				ReplicatorServiceHost = new ServiceHost(typeof(ReplicatorService));
				ReplicatorServiceHost.AddServiceEndpoint(typeof(ReplicatorServiceContract),
					new NetTcpBinding(), $"net.tcp://localhost:{ServicePort}/Replicator");
				ReplicatorServiceHost.Open();
				Console.WriteLine($"SmartCardService host ready as \"net.tcp://localhost:{ServicePort}/Replicator\"!");
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to open host at \"net.tcp://localhost:{ServicePort}/Replicator\" endpoint: {e.Message}");
				return false;
			}
			return true;
		}
		internal static bool CloseReplicatorServiceHost()
		{
			try
			{
				SCServiceHost.Close();
				Console.WriteLine("ReplicatorService host closed!");
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR while trying to close ReplicatorService host: " + e.Message);
				return false;
			}

			return true;
		}

		internal static bool CreateReplicatorProxy()
		{
			try
			{
				NetTcpBinding binding = new NetTcpBinding();
				factory = new ChannelFactory<ReplicatorServiceContract>(binding,
					new EndpointAddress($"net.tcp://localhost:{(servicePort == 5000 ? 5001 : 5000)}/Replicator"));

				ReplicatorProxy = factory.CreateChannel();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while creating a SmartCardsService communication channel: " + e.Message);
				return false;
			}

			return true;
		}

		internal static bool CloseReplicatorProxy()
		{
			try
			{
				if(factory != null)
					factory.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while creating a SmartCardsService communication channel: " + e.Message);
				return false;
			}

			return true;
		}

		internal static bool OpenValidationServiceEndpoint()
		{
			return true;
		}
		internal static bool CloseValidationServiceEndpoint()
		{
			return true;
		}

	}
}
