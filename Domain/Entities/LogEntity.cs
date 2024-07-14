using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LogEntity
	{
		public int Id { get; set; }	
		public int IdIssue { get; set; }
		public IssueEntity Issues { get; set; }//+
		public int IdUser { get; set; }
		public UserEntity User { get; set; }//+
		public string Action { get; set; }
		public string Notes { get; set; }
		public long InsertDate { get; set; }
	}
}
