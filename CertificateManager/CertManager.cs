using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManager
{
    public class CertManager
    {
		public static X509Certificate2 GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation, string subjectName)
		{

			X509Store store = new X509Store(storeName, storeLocation);
			store.Open(OpenFlags.ReadOnly);

			X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, true);

			foreach (X509Certificate2 c in certCollection)
			{
				string[] certSubjectComma = c.SubjectName.Name.Split(',');
				string[] certSubjectEquals = certSubjectComma[0].Split('=');
				if (certSubjectEquals[1].Equals(subjectName))
				{
					return c;
				}
			}

			return null;
		}
	}
}
