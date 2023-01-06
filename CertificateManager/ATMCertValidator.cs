using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManager
{
    public class ATMCertValidator : X509CertificateValidator
	{
		public override void Validate(X509Certificate2 certificate)
		{
			X509Certificate2 atmCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

			if (!certificate.Issuer.Equals(atmCert.Issuer))
			{
				throw new Exception("Certificate is not from the valid issuer.");
			}
		}
	}
}
