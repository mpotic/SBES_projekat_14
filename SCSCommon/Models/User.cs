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
		[DataMember]
		string newPin;
		double amount;
		public User() { }
		public User(string subjectName, string pin, string organizationalUnit, string newPin)
		{
			SubjectName = subjectName;
			Pin = pin;
			OrganizationalUnit = organizationalUnit;
			NewPin = newPin;
		}
		public User(User user)
		{
			SubjectName = user.SubjectName;
			Pin = user.Pin;
			OrganizationalUnit = user.OrganizationalUnit;
			NewPin = user.NewPin;
		}
		public string SubjectName { get => subjectName; set => subjectName = value; }
		public string Pin { get => pin; set => pin = value; }
		public string OrganizationalUnit { get => organizationalUnit; set => organizationalUnit = value; }
		public string NewPin { get => newPin; set => newPin = value; }
		public double Amount { get => amount; set => amount = value; }
	}
}
