using SmartCardsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Models
{
	[DataContract]
	[KnownType(typeof(User))]
	public class AllUsers
	{
		[DataMember]
		List<User> users;

		public List<User> Users { get => users; set => users = value; }
	}
}
