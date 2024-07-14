using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
	public class IssuesTypeEntity
	{
		public int Id { get; set; }	
		public int IssueLevel { get; set; }
		public string ParentIssues {  get; set; }	
		public string Name { get; set; }
		public long InsertDate { get; set; }
		public List<IssueEntity> Issues { get; set; }
	}
}
