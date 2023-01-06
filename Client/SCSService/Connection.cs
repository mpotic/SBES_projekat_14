using ATMCommon;
using SCSCommon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.SCSService
{
	internal static class Connection
	{
		static ChannelFactory<SCServiceContract> factory;
		internal static SCServiceContract ScsProxy { get; set; }

		internal static bool CreateSCServiceProxy(string connectionPort)
		{
			try
			{
				NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
				binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				binding.SendTimeout = new TimeSpan(0, 3, 0);

				factory = new ChannelFactory<SCServiceContract>(binding,
					new EndpointAddress($"net.tcp://localhost:{connectionPort}/SCService"));

				ScsProxy = factory.CreateChannel();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while creating a SmartCardsService communication channel: " + e.Message);
				return false;
			}

			return true;
		}

		internal static bool CloseSCServiceProxy()
		{
			try
			{
				factory.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while creating a SmartCardsService communication channel: " + e.Message);
				return false;
			}

			return true;
		}


	}
}
