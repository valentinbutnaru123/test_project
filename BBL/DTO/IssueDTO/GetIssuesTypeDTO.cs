using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL.DTO.IssueDTO
{
  public class GetIssuesTypeDTO
  {
		public int Id { get; set; }
		public int IssueLevel { get; set; }
		public int ParentIssues { get; set; }
		public string Name { get; set; }
		

   }
}
