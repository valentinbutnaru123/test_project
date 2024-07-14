using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class UserTypeEntity
	{
       public int Id { get; set; }	
	   public string UserType { get; set; }
	   public List<UserEntity> Users { get; set; }	//+
	   public List<IssueEntity> Issues { get; set; }//+



	}
}
