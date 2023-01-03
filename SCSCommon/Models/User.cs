using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardsService.Models
{
	[DataContract]
	public class User
	{
		[DataMember]
		string subjectName;
		[DataMember]
		string pin;
		[DataMember]
		string organizationalUnit;
		public User(string subjectName, string pin, string organizationalUnit)
		{
			SubjectName = subjectName;
			Pin = pin;
			OrganizationalUnit = organizationalUnit;
		}
		public string SubjectName { get => subjectName; set => subjectName = value; }
		public string Pin { get => pin; set => pin = value; }
		public string OrganizationalUnit { get => organizationalUnit; set => organizationalUnit = value; }
	}
}
