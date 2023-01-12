using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
        private ValidationService ValidationService { get; set; } = new ValidationService();

        public Tuple<byte[], string> GetAllUsers(string message, byte[] clientSignature)
        {
            string clientCertCN = "test";
            //string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);

            string usersToString = "";
            if (role == "Manager")
            {
                List<User> users = new List<User>();
                users = ValidationService.GetAllUsers();

                //User test1 = new User("test1", "1234", "test1", "");
                //users.Add(test1);
                //User test2 = new User("test2", "1334", "test2", "");
                //users.Add(test2);
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

            string serviceCertCN = "atmservice";
            //string serviceCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, serviceCertCN);
            byte[] signature = DigitalSignature.Create(usersToString, HashAlgorithm.SHA1, certificateSign);

            Tuple<byte[], string> tuple = new Tuple<byte[], string>(signature, usersToString);
            return tuple;
        }

        public bool ValidateSmartCardPin(string message, byte[] clientSignature)
        {
            //string clientCertCN = "test";
            string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
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
            //return true;
        }
        public bool CheckPayment(string amount, byte[] signature)
        {
            //string clientCertCN = "test";
            string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);
            if (role == "SmartCardUser" || role == "Manager")
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
                Audit.PaymentSuccess("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amountAndSubjectName[0]), clientCertCN);
                //return true;

                return ValidationService.ValidateDeposit(amountAndSubjectName[0], amountAndSubjectName[1]);
            }
            else
            {
                Audit.PaymentFailure("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amount), clientCertCN);
                return false;
            }
        }

        public bool CheckPayout(string amount, byte[] signature)
        {
            //string clientCertCN = "test";
            string clientCertCN = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            string role = CertManager.GetOrganizationalUnit(StoreName.TrustedPeople, StoreLocation.LocalMachine, clientCertCN);
            if (role == "SmartCardUser" || role == "Manager")
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
                Audit.PayoutSuccess("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amountAndSubjectName[0]), clientCertCN);
                //return true;
                return ValidationService.ValidatePayout(amountAndSubjectName[0], amountAndSubjectName[1]);
            }
            else
            {
                Audit.PayoutFailure("ATMService/" + Replication.ServiceType.ToString(), Double.Parse(amount), clientCertCN);
                return false;
            }
        }
    }
}
