using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMCommon;
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

        public bool ValidateSmartCardPin(string subjectName, string pin)
        {
            //Skinuti komentar kada se naprave sertifikati
            //return ValidationService.ValidateSmartCardPin(new User(subjectName, pin, "", ""));
            return true;
        }
    }
}
