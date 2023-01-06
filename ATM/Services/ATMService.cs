using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMCommon;
using SCSCommon;
using SmartCardsService.Models;

namespace ATM.Services
{
    internal class ATMService : ATMServiceContract
    {
        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool ValidateSmartCardPin(string subjectName, string pin)
        {
            throw new NotImplementedException();
        }
    }
}
