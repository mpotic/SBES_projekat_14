using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ATM.Connections;
using ATMCommon;
using CertificateManager;
using CustomLogger;
using SCSCommon;
using SCSCommon.Services;
using SmartCardsService.Features;
using SmartCardsService.Models;

namespace ATM.Services
{
    internal class ATMService : ATMServiceContract
    {
        public Tuple<byte[], string> GetAllUsers(string message, byte[] clientSignature)
        {
            string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            string[] certSubjectComma = clientCertCN.Split(',');
            string[] certSubjectEquals = certSubjectComma[0].Split('=');
            string clientName = certSubjectEquals[1];

            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientName);

            string usersToString = "";
            if (role == "Manager")
            {
                List<User> users = new List<User>();
                users = WCFManager.ValidationProxy.GetAllUsers();
                foreach (var user in users)
                {
                    usersToString += user.SubjectName + "+" + user.OrganizationalUnit + ";";
                }
                usersToString = usersToString.Remove(usersToString.Length - 1, 1);
            }
            else
            {
                Console.WriteLine("User is not manager!");
                return null;
            }

            string serviceCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, serviceCertCN);
            byte[] signature = DigitalSignature.Create(usersToString, HashAlgorithm.SHA1, certificateSign);

            Tuple<byte[], string> tuple = new Tuple<byte[], string>(signature, usersToString);
            return tuple;
        }

        public bool ValidateSmartCardPin(string message, byte[] clientSignature)
        {
            string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            string[] certSubjectComma = clientCertCN.Split(',');
            string[] certSubjectEquals = certSubjectComma[0].Split('=');
            string clientName = certSubjectEquals[1];
            X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientName);
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
            return WCFManager.ValidationProxy.ValidateSmartCardPin(new User(clientInfo[0], clientInfo[1], "", ""));
        }
        public bool CheckPayment(string amount, byte[] signature)
        {
            string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            string[] certSubjectComma = clientCertCN.Split(',');
            string[] certSubjectEquals = certSubjectComma[0].Split('=');
            string clientName = certSubjectEquals[1];
            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientName);
            if (role == "SmartCardUser" || role == "Manager")
            {
                X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientName);
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
                Audit.PaymentSuccess("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amountAndSubjectName[0]), clientName);


                return WCFManager.ValidationProxy.ValidateDeposit(amountAndSubjectName[0], amountAndSubjectName[1]);
            }
            else
            {
                Audit.PaymentFailure("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amount), clientName);
                return false;
            }
        }

        public bool CheckPayout(string amount, byte[] signature)
        {
            string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            string[] certSubjectComma = clientCertCN.Split(',');
            string[] certSubjectEquals = certSubjectComma[0].Split('=');
            string clientName = certSubjectEquals[1];
            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientName);
            if (role == "SmartCardUser" || role == "Manager")
            {
                X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientName);
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
                Audit.PayoutSuccess("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amountAndSubjectName[0]), clientName);

                return WCFManager.ValidationProxy.ValidatePayout(amountAndSubjectName[0], amountAndSubjectName[1]);
            }
            else
            {
                Audit.PayoutFailure("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amount), clientName);
                return false;
            }
        }
    }
}
