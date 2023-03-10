using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManager
{
    public class ClientCertValidator : X509CertificateValidator
	{
		public override void Validate(X509Certificate2 clientCertificate)
		{
			string[] certIssuer = clientCertificate.IssuerName.Name.Split('=');
			if (!certIssuer[1].Equals("SmartCardsServiceCA"))
			{
				throw new Exception("Client is not certified to be Smart Card User.");
			}
		}
	}
}
