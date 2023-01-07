using ATM.Services;
using ATMCommon;
using CertificateManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Connections
{
	internal static class WCFManager
    {

		private static int servicePort;
		private static ServiceHost atmServiceHost;

		internal static int ServicePort { get => servicePort; set => servicePort = value; }
		public static ServiceHost AtmServiceHost { get => atmServiceHost; set => atmServiceHost = value; }

		internal static bool OpenATMHost()
		{

			try
			{
				string atmCertCN = "atmservice";

				AtmServiceHost = new ServiceHost(typeof(ATMService));

				NetTcpBinding binding = new NetTcpBinding();
				binding.Security.Mode = SecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
				binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

				AtmServiceHost.AddServiceEndpoint(typeof(ATMServiceContract),
					binding, $"net.tcp://localhost:{ServicePort}/ATMService");

				AtmServiceHost.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
				AtmServiceHost.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ATMCertValidator();
				AtmServiceHost.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
				AtmServiceHost.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, atmCertCN);

				AtmServiceHost.Open();
				Console.WriteLine($"ATM Service host ready at \"net.tcp://localhost:{ServicePort}/ATMService\"!");
			}
			catch (Exception e)
			{
				Console.WriteLine($"ERROR while trying to open host at \"net.tcp://localhost:{ServicePort}/ATMService\" endpoint: {e.Message}");
				return false;
			}
			return true;
		}

		internal static bool CloseSCServiceHost()
		{
			try
			{
				AtmServiceHost.Close();
				Console.WriteLine("SmartCardService host closed!");
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR while trying to close ATM Service host: " + e.Message);
				return false;
			}

			return true;
		}
	}
}
