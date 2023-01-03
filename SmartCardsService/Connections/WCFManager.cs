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
		private static ServiceHost replicatorServiceHost;
		private static ServiceHost validationServiceHost;
		private static ChannelFactory<ReplicatorServiceContract> factory;
		private static ReplicatorServiceContract replicatorProxy;

		internal static int ServicePort { get => servicePort; set => servicePort = value; }
		public static ServiceHost SCServiceHost { get => scServiceHost; set => scServiceHost = value; }
		public static ServiceHost ReplicatorServiceHost { get => replicatorServiceHost; set => replicatorServiceHost = value; }
		internal static ServiceHost ValidationServiceHost { get => validationServiceHost; set => validationServiceHost = value; }

		internal static ReplicatorServiceContract ReplicatorProxy
		{
			get
			{
				if (replicatorProxy == null)
					WCFManager.CreateReplicatorProxy();
				return replicatorProxy;
			}
			set => replicatorProxy = value;
		}

		internal static bool OpenSCServiceHost()
		{
			try
			{
				SCServiceHost = new ServiceHost(typeof(SCService));

				NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
				binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				SCServiceHost.AddServiceEndpoint(typeof(SCServiceContract),
					binding, $"net.tcp://localhost:{ServicePort}/SCService");
				SCServiceHost.Open();
				Console.WriteLine($"SmartCardService host ready at \"net.tcp://localhost:{ServicePort}/SCService\"!");
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
			catch (Exception e)
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

				NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
				binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				ReplicatorServiceHost.AddServiceEndpoint(typeof(ReplicatorServiceContract),
					binding, $"net.tcp://localhost:{ServicePort}/ReplicatorService");
				ReplicatorServiceHost.Open();
				Console.WriteLine($"ReplicatorService host ready at \"net.tcp://localhost:{ServicePort}/ReplicatorService\"!");
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to open host at \"net.tcp://localhost:{ServicePort}/ReplicatorService\" endpoint: {e.Message}");
				return false;
			}
			return true;
		}
		internal static bool CloseReplicatorServiceHost()
		{
			try
			{
				ReplicatorServiceHost.Close();
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
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
				binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				factory = new ChannelFactory<ReplicatorServiceContract>(binding,
					new EndpointAddress($"net.tcp://localhost:{(servicePort == 5000 ? 5001 : 5000)}/ReplicatorService"));

				ReplicatorProxy = factory.CreateChannel();

				Console.WriteLine($"Successfully created ReplicatorProxy at " +
					$"\"net.tcp://localhost:{(servicePort == 5000 ? 5001 : 5000)}/ReplicatorService\"!");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while creating a ReplicatorService communication channel: " + e.Message);
				return false;
			}

			return true;
		}

		internal static bool CloseReplicatorProxy()
		{
			try
			{
				if (factory != null)
					factory.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while closing a ReplicatorService communication channel: " + e.Message);
				return false;
			}

			return true;
		}

		internal static bool OpenValidationServiceHost()
		{
			try
			{
				ValidationServiceHost = new ServiceHost(typeof(ValidationService));

				NetTcpBinding binding = new NetTcpBinding();
				//binding.Security.Mode = SecurityMode.Transport;
				//binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
				//binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				ValidationServiceHost.AddServiceEndpoint(typeof(ValidationServiceContract),
					binding, $"net.tcp://localhost:{ServicePort}/ValidationService");
				ValidationServiceHost.Open();
				Console.WriteLine($"ValidationService host ready at \"net.tcp://localhost:{ServicePort}/ValidationService\"!");
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to open host at \"net.tcp://localhost:{ServicePort}/ValidationService\" endpoint: {e.Message}");
				return false;
			}
			return true;
		}
		internal static bool CloseValidationServiceHost()
		{
			try
			{
				ValidationServiceHost.Close();
				Console.WriteLine("ValidationService host closed!");
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR while trying to close ValidationService host: " + e.Message);
				return false;
			}

			return true;
		}

	}
}
