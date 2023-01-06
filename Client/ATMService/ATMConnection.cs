using ATMCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.ATMService
{
    internal static class ATMConnection
    {
		static ChannelFactory<ATMServiceContract> factoryATM;
		internal static ATMServiceContract ATMProxy { get; set; }

		internal static bool CreateATMServiceProxy(string connectionPort)
		{
			try
			{
				NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
				binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				binding.SendTimeout = new TimeSpan(0, 3, 0);

				factoryATM = new ChannelFactory<ATMServiceContract>(binding,
					new EndpointAddress($"net.tcp://localhost:{connectionPort}/ATMService"));

				ATMProxy = factoryATM.CreateChannel();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while creating a ATM Service communication channel: " + e.Message);
				return false;
			}

			return true;
		}

		internal static bool CloseATMServiceProxy()
		{
			try
			{
				factoryATM.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error while creating a ATM Service communication channel: " + e.Message);
				return false;
			}

			return true;
		}
	}
}
