
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	
	public class UserEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		//public string Password { get; set; }
		public string Login { get; set; }
		public string Telephone { get; set; }	
		public int UserTypeId { get; set; }	
		public UserTypeEntity UserType { get; set; }//+
		public List <IssueEntity> Issues { get; set; }//+
		public List<LogEntity> Logs { get; set; }//+
		public long? DeleteAt { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] Salt { get; set; }
	}
}
