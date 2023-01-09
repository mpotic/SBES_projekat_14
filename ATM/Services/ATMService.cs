using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ATMCommon;
using CertificateManager;
using SCSCommon;
using SCSCommon.Services;
using SmartCardsService.Models;

namespace ATM.Services
{
    internal class ATMService : ATMServiceContract
    {
        private ValidationService ValidationService { get; set; } = new ValidationService();

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool ValidateSmartCardPin(string message, byte[] clientSignature)
        {
            //string clientCertCN = "test";
            string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);
            if (DigitalSignature.Verify(message, HashAlgorithm.SHA1, clientSignature, clientCertificate))
            {
                Console.WriteLine("Sign is valid");
            }
            else
            {
                Console.WriteLine("Sign is invalid");
                return false;
            }
            string[] clientInfo = message.Split('+');
            return ValidationService.ValidateSmartCardPin(new User(clientInfo[0], clientInfo[1], "", ""));
        }
        public bool CheckPayment(string amount, byte[] signature)
        {
            //string clientCertCN = "test";
            string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);
            if(role == "SmartCardUser" || role=="Manager")
            {
                X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);
                if (DigitalSignature.Verify(amount, HashAlgorithm.SHA1, signature, clientCertificate))
                {
                    Console.WriteLine("Sign is valid");
                }
                else
                {
                    Console.WriteLine("Sign is invalid");
                    return false;
                }
                string[] amountAndSubjectName = amount.Split('+');

                return true;

                //return ValidationService.CheckPayment(amountAndSubjectName[0], amountAndSubjectName[1]);
            }
            else
            {
                return false;
            }
        }

        public bool CheckPayout(string amount, byte[] signature)
        {
            //string clientCertCN = "test";
            string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);
            if (role == "SmartCardUser" || role=="Manager")
            {
                X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);
                if (DigitalSignature.Verify(amount, HashAlgorithm.SHA1, signature, clientCertificate))
                {
                    Console.WriteLine("Sign is valid");
                }
                else
                {
                    Console.WriteLine("Sign is invalid");
                    return false;
                }
                string[] amountAndSubjectName = amount.Split('+');
                return true;
                //return ValidationService.CheckPayout(amountAndSubjectName[0], amountAndSubjectName[1]);
            }
            else
            {
                return false;
            }
        }
    }
}
