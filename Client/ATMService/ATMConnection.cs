using ATMCommon;
using CertificateManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
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
				string ATMCertCN = "atmservice";

				NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
				binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				X509Certificate2 atmCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, ATMCertCN);
				EndpointAddress address = new EndpointAddress(new Uri($"net.tcp://localhost:{connectionPort}/ATMService"),
										  new X509CertificateEndpointIdentity(atmCert));


				binding.SendTimeout = new TimeSpan(0, 3, 0);

				factoryATM = new ChannelFactory<ATMServiceContract>(binding, address);

				string clientCertCN = "test";
				//string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

				factoryATM.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
				factoryATM.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
				factoryATM.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

				factoryATM.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);

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
