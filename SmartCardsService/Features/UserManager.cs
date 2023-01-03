using SmartCardsService.Connections;
using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Features
{
	internal class UserManager
	{
		private string MakeHash(string value)
		{
			string hash = "";
			foreach (byte b in SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(value)))
			{
				hash += $"{b:X2}";
			}

			return hash;
		}
		internal bool RegisterNewUser(User user)
		{
			user.Pin = this.MakeHash(user.Pin);

			return DatabaseCRUD.InsertNewUser(user);
		}

		internal bool CheckPinValidity(User user)
		{

			user.Pin = this.MakeHash(user.Pin);

			return DatabaseCRUD.ExistsUserWithPin(user);
		}

		internal bool ChangeUserPin(User user)
		{
			if (this.CheckPinValidity(user))
			{
				user.NewPin = this.MakeHash(user.NewPin);
				return DatabaseCRUD.UpdateUserPin(user);
			}
				

			return false;
		}
	}
}
