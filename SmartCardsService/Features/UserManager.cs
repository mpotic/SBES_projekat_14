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
		internal bool RegisterNewUser(User user)
		{
			string hash = "";
			foreach (byte b in SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(user.Pin)))
			{
				hash += $"{b:X2}";
			}
			user.Pin = hash;

			return DatabaseCRUD.InsertNewUser(user);
		}
	}
}
